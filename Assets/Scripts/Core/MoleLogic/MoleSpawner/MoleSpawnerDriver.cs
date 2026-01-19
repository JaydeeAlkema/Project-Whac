using System.Collections.Generic;
using Core.MoleLogic.Mole;
using Core.Time;
using NaughtyAttributes;
using UnityEngine;

namespace Core.MoleLogic.MoleSpawner
{
	public class MoleSpawnerDriver : MonoBehaviour
	{
		[BoxGroup("Settings")]
		[SerializeField] private float _spawnTime = 1.5f;

		[BoxGroup("Settings")]
		[SerializeField] private float _hideTime = 1f;

		[BoxGroup("Settings")]
		[SerializeField] private List<MoleDriver> _moles = new();

		private IMoleSpawner _moleSpawner;
		private ITimeProvider _timeProvider;

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

			_canUpdate = true;
		}

		private void Update()
		{
			if (!_canUpdate)
				return;

			_moleSpawner.Tick(_timeProvider.DeltaTime);
		}
	}
}
