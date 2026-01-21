using System;
using EventArgs;
using EventArgs.GameLoop;
using UnityEngine;
using Utils;

namespace Core.Score
{
	public sealed class ScoreService : IScoreService
	{
		public event Action<int> ScoreChanged;
		public int Score { get; private set; }

		private readonly IDisposable _onMoleHitDisposable;
		private readonly IDisposable _gameRestartDisposable;

		public ScoreService(IEventBus bus)
		{
			_gameRestartDisposable = bus.Subscribe<GameOnRestartEventArgs>(OnGameRestart);
			_onMoleHitDisposable = bus.Subscribe<MoleOnHitEventArgs>(OnMoleHit);
		}

		public void Dispose()
		{
			_onMoleHitDisposable.Dispose();
			_gameRestartDisposable.Dispose();
		}

		private void OnGameRestart(GameOnRestartEventArgs _)
		{
			Score = 0;
			ScoreChanged?.Invoke(Score);
		}

		private void OnMoleHit(MoleOnHitEventArgs args)
		{
			Score += args.Score;
			ScoreChanged?.Invoke(Score);
		}

		public ScoreEntry CreateEntry(string username)
		{
			return new()
			{
				_username = username,
				_score = Score,
			};
		}
	}
}
