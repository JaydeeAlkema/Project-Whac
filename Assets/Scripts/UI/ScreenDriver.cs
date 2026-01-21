using System;
using Core.GameComposition;
using Core.Score;
using EventArgs.GameLoop;
using EventArgs.Timer;
using UI.GameOver;
using UI.GameScreen.Score;
using UI.GameScreen.Timer;
using UI.HighScores;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
	public sealed class ScreenDriver : MonoBehaviour
	{
		[SerializeField] private UIDocument _ui;
		[SerializeField] private GameCompositionRoot _compositionRoot;
		[SerializeField] private VisualTreeAsset _highScoreRowTemplate;

		private VisualElement _game;
		private VisualElement _gameOver;
		private VisualElement _highScores;

		private ScoreScope _gameScoreScope;
		private ScoreScope _gameOverScoreScope;

		private TimerScope _timerScope;

		private GameOverScope _gameOverScope;

		private HighScoresScope _highScoresScope;

		private IDisposable _timerStartedDisposable;
		private IDisposable _timerEndedDisposable;

		private void Start()
		{
			VisualElement root = _ui.rootVisualElement;
			CacheVisualElements(root);

			_gameScoreScope = new(_compositionRoot.ScoreService, _game);
			_gameOverScoreScope = new(_compositionRoot.ScoreService, _gameOver);

			_timerScope = new(_compositionRoot.TimerService, _game);

			_gameOverScope = new(_gameOver);

			_timerStartedDisposable = _compositionRoot.EventBus.Subscribe<TimerStartedEventArgs>(OnTimerStarted, replayLast: true);
			_timerEndedDisposable = _compositionRoot.EventBus.Subscribe<TimerEndedEventArgs>(OnTimerEnded, replayLast: true);

			_gameOverScope.ContinueRequested += GameOverScope_OnContinueRequested;
		}

		private void OnDestroy()
		{
			_gameScoreScope?.Dispose();
			_gameOverScoreScope?.Dispose();

			_timerScope?.Dispose();

			_gameOverScope.ContinueRequested -= GameOverScope_OnContinueRequested;
			_gameOverScope?.Dispose();

			_highScoresScope.RestartRequested -= HighScoresScope_OnRestartRequested;
			_highScoresScope?.Dispose();

			_timerEndedDisposable?.Dispose();
			_timerStartedDisposable?.Dispose();
		}

		private void GameOverScope_OnContinueRequested()
		{
			ScoreEntry entry = _compositionRoot.ScoreService.CreateEntry(_gameOverScope.Username);
			_compositionRoot.ScoreRepository.Add(entry);

			_highScoresScope = new(_highScores, _compositionRoot.ScoreRepository.GetAll(), _highScoreRowTemplate);
			_highScoresScope.RestartRequested += HighScoresScope_OnRestartRequested;

			ToggleHighScores();
		}

		private void HighScoresScope_OnRestartRequested()
		{
			_compositionRoot.EventBus.PublishSticky(new GameOnRestartEventArgs());
		}

		private void OnTimerStarted(TimerStartedEventArgs _)
		{
			ToggleGameScreen();
		}

		private void OnTimerEnded(TimerEndedEventArgs _)
		{
			ToggleGameOverScreen();
		}

		private void CacheVisualElements(VisualElement root)
		{
			_game = root.Q<VisualElement>("game");
			_gameOver = root.Q<VisualElement>("gameOver");
			_highScores = root.Q<VisualElement>("highScores");
		}

		private void ToggleGameScreen()
		{
			_game.EnableInClassList("hidden", false);
			_gameOver.EnableInClassList("hidden", true);
			_highScores.EnableInClassList("hidden", true);
		}

		private void ToggleGameOverScreen()
		{
			_game.EnableInClassList("hidden", true);
			_gameOver.EnableInClassList("hidden", false);
			_highScores.EnableInClassList("hidden", true);
		}

		private void ToggleHighScores()
		{
			_game.EnableInClassList("hidden", true);
			_gameOver.EnableInClassList("hidden", true);
			_highScores.EnableInClassList("hidden", false);
		}
	}
}
