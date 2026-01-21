using System.Collections.Generic;
using Core.MoleLogic.Mole;
using Core.Random;
using UnityEngine;

namespace Core.MoleLogic.MoleSpawner
{
	public sealed class MoleSpawner : IMoleSpawner
	{
		private readonly List<IMole> _moles;
		private readonly SystemRandomProvider _random;
		private float _spawnTimer;
		private float _hideTimer;

		private IMole _activeMole;

		private readonly float _spawnTimeInterval;
		private readonly float _hideTime;

		public MoleSpawner(List<IMole> moles, SystemRandomProvider random, float spawnTimeInterval, float hideTime)
		{
			_moles = moles;
			_random = random;
			_spawnTimeInterval = spawnTimeInterval;
			_hideTime = hideTime;
		}

		public void Tick(float deltaTime)
		{
			_spawnTimer += deltaTime;
			if (_activeMole == null && _spawnTimer >= _spawnTimeInterval)
			{
				_spawnTimer = 0f;
				ShowRandomMole();
			}

			if (_activeMole == null)
				return;

			_hideTimer += deltaTime;
			if (_hideTimer < _hideTime)
				return;

			HideActiveMole();
		}

		public void Reset()
		{
			_spawnTimer = 0f;
			_hideTimer = 0f;

			if (_activeMole == null)
				return;

			_activeMole.Hide();
			_activeMole = null;
		}

		private void ShowRandomMole()
		{
			if (_activeMole != null)
			{
				Debug.LogError("Trying to show a mole when there is already an active mole!");
				return;
			}

			int randMoleIndex = _random.Range(0, _moles.Count);
			_activeMole = _moles[randMoleIndex];
			_activeMole.Show();
			_hideTimer = 0f;
		}

		private void HideActiveMole()
		{
			if (_activeMole == null)
			{
				Debug.LogError("Trying to hide a mole when there is no active mole!");
				return;
			}

			_activeMole.Hide();
			_activeMole = null;
			_hideTimer = 0f;
		}
	}
}
