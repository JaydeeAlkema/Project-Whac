using Core.GameComposition;
using Core.Time;
using EventArgs;
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

		private void Start()
		{
			_eventBus = _compositionRoot.EventBus;
			_timeProvider = new TimeProvider();
			_timer = _timerStartValue;
			Debug.Assert(_timerStartValue > 0, "[TimerDriver] Timer start value must be greater than zero.");
			_eventBus.PublishSticky(new TimerStartedEventArgs());
		}

		private void Update()
		{
			_timer -= _timeProvider.DeltaTime;
			_timer = Mathf.Max(0f, _timer);

			_eventBus.PublishSticky(new TimerOnTickEventArgs { Time = _timer, });

			if (_timer > 0)
				return;

			_eventBus.PublishSticky(new TimerEndedEventArgs());
			enabled = false;
		}
	}
}
