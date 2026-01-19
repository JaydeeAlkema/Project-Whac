using System;
using System.Globalization;
using UnityEngine;
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
			Debug.Assert(_timerLabel != null, "Timer label not found in UXML");

			_viewModel.OnTimerTextChanged += OnTimerTextChanged;
		}

		private void OnTimerTextChanged(string text)
		{
			_timerLabel.text = text;
		}

		public void Dispose()
		{
			_viewModel.OnTimerTextChanged -= OnTimerTextChanged;
		}
	}
}
