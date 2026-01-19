using System;
using EventArgs;
using Utils;

namespace Core.Timer
{
	public sealed class TimerService : ITimerService
	{
		public event Action<float> OnTick;
		public float Time { get; private set; }

		private readonly IDisposable _subscription;

		public TimerService(IEventBus bus)
		{
			_subscription = bus.Subscribe<TimerOnTickEventArgs>(OnTimerTick);
		}

		private void OnTimerTick(TimerOnTickEventArgs args)
		{
			Time = args.Time;
			OnTick?.Invoke(Time);
		}

		public void Dispose()
		{
			_subscription.Dispose();
		}
	}
}
