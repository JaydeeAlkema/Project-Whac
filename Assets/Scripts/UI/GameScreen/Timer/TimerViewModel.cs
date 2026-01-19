using System;
using Core.Timer;
using UnityEngine;

namespace UI.GameScreen.Timer
{
	public sealed class TimerViewModel : IDisposable
	{
		public float Timer { get; private set; }
		public event Action<string> OnTimerTextChanged;

		private readonly ITimerService _timerService;
		private string _timerText;

		public TimerViewModel(ITimerService timerService)
		{
			_timerService = timerService;
			Timer = _timerService.Time;

			_timerService.OnTick += OnTimerTick;
		}

		private void OnTimerTick(float time)
		{
			UpdateTimerText(time);
		}

		private void UpdateTimerText(float time)
		{
			Timer = time;
			_timerText = Mathf.CeilToInt(time).ToString();
			OnTimerTextChanged?.Invoke(_timerText);
		}

		public void Dispose()
		{
			_timerService.OnTick -= OnTimerTick;
		}
	}
}
