using System;
using Core.Score;
using UnityEngine.UIElements;

namespace UI.GameScreen.Score
{
	public sealed class ScoreScope : IDisposable
	{
		private readonly ScoreViewModel _viewModel;
		private readonly ScoreUIBinder _binder;

		public ScoreScope(IScoreService scoreService, VisualElement root)
		{
			_viewModel = new(scoreService);
			_binder = new(root, _viewModel);
		}

		public void Dispose()
		{
			_binder.Dispose();
			_viewModel.Dispose();
		}
	}
}
