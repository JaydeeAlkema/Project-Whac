using System;
using System.Globalization;
using UnityEngine.UIElements;

namespace UI.GameScreen.Timer
{
	public sealed class TimerUIBinder : IDisposable
	{
		private readonly Label _timerLabel;
		private readonly TimerViewModel _viewModel;

		public TimerUIBinder(VisualElement root, TimerViewModel viewModel)
		{
			_viewModel = viewModel;
			_timerLabel = root.Q<Label>("timer");

			_timerLabel.text = _viewModel.Timer.ToString("F1", CultureInfo.InvariantCulture);
			_viewModel.OnTick += OnTimerTick;
		}

		private void OnTimerTick(float time)
		{
			if (time <= 0)
			{
				_timerLabel.text = "0";
				return;
			}

			_timerLabel.text = _viewModel.Timer.ToString("F1", CultureInfo.InvariantCulture);
		}

		public void Dispose()
		{
			_viewModel.OnTick -= OnTimerTick;
		}
	}
}
