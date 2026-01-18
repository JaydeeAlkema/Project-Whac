using System;

namespace Utils
{
	internal sealed class SubscriptionToken<T> : IDisposable
	{
		private readonly IEventBus _bus;
		private readonly Action<T> _listener;
		private bool _disposed;

		public SubscriptionToken(IEventBus bus, Action<T> listener)
		{
			_bus = bus;
			_listener = listener;
		}

		public void Dispose()
		{
			if (_disposed) return;

			_bus.Unsubscribe(_listener);
			_disposed = true;
		}
	}
}
