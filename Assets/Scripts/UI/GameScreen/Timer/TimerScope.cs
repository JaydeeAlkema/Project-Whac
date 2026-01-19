using System;
using Core.Timer;
using UnityEngine.UIElements;

namespace UI.GameScreen.Timer
{
	public sealed class TimerScope : IDisposable
	{
		private readonly TimerViewModel _viewModel;
		private readonly TimerUIBinder _binder;

		public TimerScope(ITimerService timerService, VisualElement root)
		{
			_viewModel = new(timerService);
			_binder = new(root, _viewModel);
		}

		public void Dispose()
		{
			_binder.Dispose();
			_viewModel.Dispose();
		}
	}
}
