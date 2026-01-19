using System;
using Core.Score;

namespace UI.GameScreen.Score
{
	public sealed class ScoreViewModel : IDisposable
	{
		public int Score { get; private set; }
		public event Action<int> ScoreChanged;

		private readonly IScoreService _scoreService;

		public ScoreViewModel(IScoreService scoreService)
		{
			_scoreService = scoreService;
			Score = _scoreService.Score;

			_scoreService.ScoreChanged += OnScoreChanged;
		}

		private void OnScoreChanged(int newScore)
		{
			Score = newScore;
			ScoreChanged?.Invoke(newScore);
		}

		public void Dispose()
		{
			_scoreService.ScoreChanged -= OnScoreChanged;
		}
	}
}
