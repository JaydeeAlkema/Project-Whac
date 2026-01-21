using System;
using System.Collections.Generic;
using Core.GameComposition;
using Core.MoleLogic.Mole;
using Core.Time;
using EventArgs.Timer;
using NaughtyAttributes;
using UnityEngine;

namespace Core.MoleLogic.MoleSpawner
{
	public class MoleSpawnerDriver : MonoBehaviour
	{
		[BoxGroup("Dependencies")]
		[SerializeField] private GameCompositionRoot _root;

		[BoxGroup("Settings")]
		[SerializeField] private float _spawnTime = 1.5f;

		[BoxGroup("Settings")]
		[SerializeField] private float _hideTime = 1f;

		[BoxGroup("Settings")]
		[SerializeField] private List<MoleDriver> _moles = new();

		private IMoleSpawner _moleSpawner;
		private ITimeProvider _timeProvider;

		private IDisposable _timerEndedDisposable;
		private IDisposable _timerStartedDisposable;

		private bool _canUpdate;

		private void Start()
		{
			foreach (MoleDriver mole in _moles) mole.Initialize();
			List<IMole> moleInterfaces = _moles.ConvertAll(moleDriver => moleDriver.Mole);
			_timeProvider = new TimeProvider();

			_moleSpawner = new MoleSpawner(
				moleInterfaces,
				new(),
				_spawnTime,
				_hideTime
			);

			_timerStartedDisposable = _root.EventBus.Subscribe<TimerStartedEventArgs>(OnTimerStarted, replayLast: true);
			_timerEndedDisposable = _root.EventBus.Subscribe<TimerEndedEventArgs>(OnTimerEnded, replayLast: true);
		}

		private void Update()
		{
			if (!_canUpdate)
				return;

			_moleSpawner.Tick(_timeProvider.DeltaTime);
		}

		private void OnDestroy()
		{
			_timerEndedDisposable?.Dispose();
			_timerStartedDisposable?.Dispose();
		}

		private void OnTimerStarted(TimerStartedEventArgs _)
		{
			_canUpdate = true;
			_moleSpawner.Reset();
		}

		private void OnTimerEnded(TimerEndedEventArgs _)
		{
			_canUpdate = false;
			_moleSpawner.Reset();
		}
	}
}
