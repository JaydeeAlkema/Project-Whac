using System;

namespace Utils
{
	public interface IEventBus
	{
		IDisposable Subscribe<T>(Action<T> listener, int priority = 0, bool replayLast = false);
		void Unsubscribe<T>(Action<T> listener);
		void Publish<T>(T evt);
		void PublishSticky<T>(T evt);
		void Clear();
	}
}
