using System;

namespace Utils
{
	/// <summary>
	///     Token representing a subscription to an event. Disposing the token will unsubscribe the listener.
	/// </summary>
	/// <typeparam name="T"> The type of the event. </typeparam>
	internal sealed class SubscriptionToken<T> : IDisposable
	{
		private readonly Action<T> _listener;
		private bool _disposed;

		public SubscriptionToken(Action<T> listener)
		{
			_listener = listener;
		}

		public void Dispose()
		{
			if (_disposed)
				return;

			EventBus.Unsubscribe(_listener);
			_disposed = true;
		}
	}
}
