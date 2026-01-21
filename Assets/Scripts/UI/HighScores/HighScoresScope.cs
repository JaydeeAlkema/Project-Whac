using System;
using System.Collections.Generic;
using Core.Score;
using UnityEngine.UIElements;

namespace UI.HighScores
{
	public sealed class HighScoresScope : IDisposable
	{
		public event Action RestartRequested;

		private readonly HighScoresUIBinder _binder;

		public HighScoresScope(
			VisualElement root,
			IReadOnlyList<ScoreEntry> entries,
			VisualTreeAsset rowTemplate)
		{
			HighScoresViewModel viewModel = new(entries);
			_binder = new(root, viewModel, rowTemplate);

			_binder.RestartClicked += BinderOnRestartClicked;
		}

		private void BinderOnRestartClicked()
		{
			RestartRequested?.Invoke();
		}

		public void Dispose()
		{
			_binder?.Dispose();
		}
	}
}
