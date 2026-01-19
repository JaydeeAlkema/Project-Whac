using System;
using Core.Timer;

namespace UI.GameScreen.Timer
{
	public sealed class TimerViewModel : IDisposable
	{
		public float Timer { get; private set; }
		public event Action<float> OnTick;
		
		private readonly ITimerService _timerService;

		public TimerViewModel(ITimerService timerService)
		{
			_timerService = timerService;
			Timer = _timerService.Time;
			
			_timerService.OnTick += OnTimerTick;
		}

		private void OnTimerTick(float time)
		{
			Timer = time;
			OnTick?.Invoke(time);
		}

		public void Dispose()
		{
			_timerService.OnTick -= OnTick;
			_timerService.Dispose();
		}
	}
}
