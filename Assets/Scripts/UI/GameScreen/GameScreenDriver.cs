using System;
using Core.GameComposition;
using EventArgs.Timer;
using UI.GameScreen.Score;
using UI.GameScreen.Timer;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.GameScreen
{
	public sealed class GameScreenDriver : MonoBehaviour
	{
		[SerializeField] private UIDocument _ui;
		[SerializeField] private GameCompositionRoot _compositionRoot;

		private VisualElement _game;
		private VisualElement _gameOver;

		private ScoreScope _scoreScope;
		private TimerScope _timerScope;

		private IDisposable _timerEndedDisposable;

		private void Start()
		{
			VisualElement root = _ui.rootVisualElement;

			_scoreScope = new(_compositionRoot.ScoreService, root);
			_timerScope = new(_compositionRoot.TimerService, root);

			CacheVisualElements(root);
			ToggleGameScreen();

			_timerEndedDisposable = _compositionRoot.EventBus.Subscribe<TimerEndedEventArgs>(OnTimerEnded);
		}

		private void OnDestroy()
		{
			_scoreScope?.Dispose();
			_timerScope?.Dispose();
			_timerEndedDisposable?.Dispose();
		}

		private void OnTimerEnded(TimerEndedEventArgs _)
		{
			ToggleGameOverScreen();
		}

		private void CacheVisualElements(VisualElement root)
		{
			_game = root.Q<VisualElement>("game");
			_gameOver = root.Q<VisualElement>("gameOver");
		}

		private void ToggleGameScreen()
		{
			_game.EnableInClassList("hidden", false);
			_gameOver.EnableInClassList("hidden", true);
		}

		private void ToggleGameOverScreen()
		{
			_game.EnableInClassList("hidden", true);
			_gameOver.EnableInClassList("hidden", false);
		}
	}
}
