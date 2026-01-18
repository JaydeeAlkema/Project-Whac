using System.Collections.Generic;
using System.Linq;
using Core.MoleLogic.Mole;
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

		private IMoleSpawner _moleSpawner;

		private void Start()
		{
			List<IMole> moles = GetComponentsInChildren<MoleDriver>().Select(m => m.Mole).ToList();

			_moleSpawner = new MoleSpawner(
				moles,
				new(),
				_spawnTime,
				_hideTime
			);
		}

		private void Update()
		{
			_moleSpawner?.Tick(UnityEngine.Time.deltaTime);
		}
	}
}
