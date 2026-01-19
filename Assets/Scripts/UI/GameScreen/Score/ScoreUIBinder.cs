using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.GameScreen.Score
{
	public sealed class ScoreUIBinder : IDisposable
	{
		private readonly Label _scoreLabel;
		private readonly ScoreViewModel _viewModel;

		public ScoreUIBinder(VisualElement root, ScoreViewModel viewModel)
		{
			_viewModel = viewModel;
			_scoreLabel = root.Q<Label>("score");
			Debug.Assert(_scoreLabel != null, "Score label not found in UXML");


			_scoreLabel.text = _viewModel.Score.ToString();
			_viewModel.ScoreChanged += OnScoreChanged;
		}

		private void OnScoreChanged(int newScore)
		{
			_scoreLabel.text = newScore.ToString();
		}

		public void Dispose()
		{
			_viewModel.ScoreChanged -= OnScoreChanged;
		}
	}
}
