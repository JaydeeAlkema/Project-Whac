using System;
using System.Collections.Generic;

namespace Utils
{
	/// <summary>
	///     I really enjoy using eventbuses. I'm aware that they can eventually be a pain in the ass.
	///     however, I figured that for a simple/small game like this, it's absolutely fine to make use of.
	///     Even though I kind of already abused this for "GameOnStartEventArgs" & "GameOnRestartEventArgs".
	///     Because those events are meant for control flow. The difference between these events and others, is that
	///     these can be interpreted as commands, not facts. "MoleOnHitEventArgs" is a fact, "GameOnStartEventArgs" is not.
	/// </summary>
	public sealed class EventBus : IEventBus
	{
		private class Subscription
		{
			public Delegate Callback;
			public int Priority;
		}

		private readonly Dictionary<Type, List<Subscription>> _events = new();
		private readonly Dictionary<Type, object> _lastEvents = new();

		public IDisposable Subscribe<T>(Action<T> listener, int priority = 0, bool replayLast = false)
		{
			Type type = typeof(T);

			if (!_events.TryGetValue(type, out List<Subscription> subs))
			{
				subs = new();
				_events[type] = subs;
			}

			if (subs.Exists(s => s.Callback.Equals(listener)))
				return new NoOpDisposable(); // just return a disposable that does nothing

			Subscription subscription = new() { Callback = listener, Priority = priority, };
			subs.Add(subscription);
			subs.Sort((a, b) => b.Priority.CompareTo(a.Priority));

			if (replayLast && _lastEvents.TryGetValue(type, out object last))
				listener((T)last);

			return new SubscriptionToken<T>(this, listener);
		}

		public void Unsubscribe<T>(Action<T> listener)
		{
			Type type = typeof(T);
			if (!_events.TryGetValue(type, out List<Subscription> subs))
				return;

			subs.RemoveAll(s => s.Callback.Equals(listener));

			if (subs.Count == 0)
				_events.Remove(type);
		}

		public void Publish<T>(T evt)
		{
			if (!_events.TryGetValue(typeof(T), out List<Subscription> subs))
				return;

			foreach (Subscription sub in new List<Subscription>(subs))
				(sub.Callback as Action<T>)?.Invoke(evt);
		}

		public void PublishSticky<T>(T evt)
		{
			_lastEvents[typeof(T)] = evt;
			Publish(evt);
		}

		public void Clear()
		{
			_events.Clear();
			_lastEvents.Clear();
		}

		// disposable that does nothing.
		// We "promise" to return a IDisposable when calling Subscribe.
		// By returning this, we avoid null checks in the caller code when subscribing multiple times with the same listener.
		// You could argue that this isn't ideal. As it will now return something that does nothing.
		// However, the idea is that when a duplicate subscription is registered, it shouldn't do something.
		// So this is a compromise to keep the code clean in the caller side.
		private sealed class NoOpDisposable : IDisposable
		{
			public void Dispose()
			{
			}
		}
	}
}
