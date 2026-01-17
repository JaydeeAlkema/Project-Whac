using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	public static class EventBus
	{
		private class Subscription
		{
			public Delegate Callback;
			public int Priority;
		}

		private static readonly Dictionary<Type, List<Subscription>> Events = new();
		private static readonly Dictionary<Type, object> LastEvents = new();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Reset()
		{
			Events.Clear();
			LastEvents.Clear();
		}

		public static void Subscribe<T>(Action<T> listener, int priority = 0, bool replayLast = false)
		{
			Type type = typeof(T);

			if (!Events.TryGetValue(type, out List<Subscription> subs))
			{
				subs = new();
				Events[type] = subs;
			}

			if (subs.Exists(s => s.Callback.Equals(listener)))
			{
				#if UNITY_EDITOR
				Debug.LogWarning($"Duplicate subscription to event {type.Name}");
				#endif
				return;
			}

			subs.Add(new()
			{
				Callback = listener,
				Priority = priority,
			});

			subs.Sort((a, b) => b.Priority.CompareTo(a.Priority));

			if (replayLast && LastEvents.TryGetValue(type, out object lastEvent))
				listener((T)lastEvent);
		}

		public static IDisposable SubscribeDisposable<T>(
			Action<T> listener,
			int priority = 0,
			bool replayLast = false)
		{
			Subscribe(listener, priority, replayLast);
			return new SubscriptionToken<T>(listener);
		}

		public static void Unsubscribe<T>(Action<T> listener)
		{
			Type type = typeof(T);
			if (!Events.TryGetValue(type, out List<Subscription> subs))
				return;

			subs.RemoveAll(s => s.Callback.Equals(listener));

			if (subs.Count == 0)
				Events.Remove(type);
		}

		public static void Publish<T>(T publishedEvent)
		{
			PublishInternal(publishedEvent, false);
		}

		public static void PublishSticky<T>(T publishedEvent)
		{
			PublishInternal(publishedEvent, true);
		}

		private static void PublishInternal<T>(T publishedEvent, bool remember)
		{
			if (remember)
				LastEvents[typeof(T)] = publishedEvent;

			if (!Events.TryGetValue(typeof(T), out List<Subscription> subs))
				return;

			List<Subscription> snapshot = new(subs);
			foreach (Subscription sub in snapshot)
				(sub.Callback as Action<T>)?.Invoke(publishedEvent);
		}

		public static void ClearAll()
		{
			Events.Clear();
			LastEvents.Clear();
		}
	}
}
