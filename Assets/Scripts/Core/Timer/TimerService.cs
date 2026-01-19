using System;
using EventArgs.Timer;
using Utils;

namespace Core.Timer
{
	public sealed class TimerService : ITimerService
	{
		public event Action<float> OnTick;
		public event Action<float> OnStarted;
		public event Action<float> OnEnded;
		public float Time { get; private set; }

		private readonly IDisposable _onTimerTickSubscription;
		private readonly IDisposable _onTimerStartedSubscription;
		private readonly IDisposable _onTimerEndedSubscription;

		public TimerService(IEventBus bus)
		{
			_onTimerTickSubscription = bus.Subscribe<TimerOnTickEventArgs>(OnTimerTick);
			_onTimerStartedSubscription = bus.Subscribe<TimerStartedEventArgs>(OnTimerStarted);
			_onTimerEndedSubscription = bus.Subscribe<TimerEndedEventArgs>(OnTimerEnded);
		}

		private void OnTimerTick(TimerOnTickEventArgs args)
		{
			Time = args.Time;
			OnTick?.Invoke(Time);
		}

		private void OnTimerStarted(TimerStartedEventArgs args)
		{
			Time = args.Time;
			OnStarted?.Invoke(Time);
		}

		private void OnTimerEnded(TimerEndedEventArgs args)
		{
			Time = args.Time;
			OnEnded?.Invoke(Time);
		}

		public void Dispose()
		{
			_onTimerTickSubscription.Dispose();
			_onTimerStartedSubscription.Dispose();
			_onTimerEndedSubscription.Dispose();
		}
	}
}
