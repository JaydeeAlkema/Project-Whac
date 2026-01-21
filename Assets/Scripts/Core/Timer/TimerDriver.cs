using System;
using Core.GameComposition;
using Core.Time;
using EventArgs.GameLoop;
using EventArgs.Timer;
using NaughtyAttributes;
using UnityEngine;
using Utils;

namespace Core.Timer
{
	public sealed class TimerDriver : MonoBehaviour
	{
		[BoxGroup("Dependencies")]
		[SerializeField] private GameCompositionRoot _compositionRoot;

		[BoxGroup("Settings")]
		[SerializeField] private float _timerStartValue;

		private IEventBus _eventBus;
		private ITimeProvider _timeProvider;
		private float _timer;

		private IDisposable _gameStartDisposable;
		private IDisposable _gameRestartDisposable;

		private bool _canUpdate;

		private void Start()
		{
			_eventBus = _compositionRoot.EventBus;
			_gameStartDisposable = _eventBus.Subscribe<GameOnStartEventArgs>(OnGameStart, replayLast: true);
			_gameRestartDisposable = _eventBus.Subscribe<GameOnRestartEventArgs>(OnGameRestart, replayLast: true);
		}

		private void Update()
		{
			if (!_canUpdate)
				return;

			_timer -= _timeProvider.DeltaTime;
			_timer = Mathf.Max(0f, _timer);

			_eventBus.PublishSticky(new TimerOnTickEventArgs { Time = _timer, });

			if (_timer > 0)
				return;

			_eventBus.PublishSticky(new TimerEndedEventArgs());
			_canUpdate = false;
		}

		private void OnDestroy()
		{
			_gameStartDisposable?.Dispose();
			_gameRestartDisposable?.Dispose();
		}

		private void OnGameStart(GameOnStartEventArgs _)
		{
			Initialize();
		}

		private void OnGameRestart(GameOnRestartEventArgs _)
		{
			_eventBus.PublishSticky(new TimerStartedEventArgs());
			_timer = _timerStartValue;
			_canUpdate = true;
		}

		private void Initialize()
		{
			_timeProvider = new TimeProvider();
			_timer = _timerStartValue;
			_eventBus.PublishSticky(new TimerStartedEventArgs());
			_canUpdate = true;
			_gameStartDisposable?.Dispose();
		}
	}
}
