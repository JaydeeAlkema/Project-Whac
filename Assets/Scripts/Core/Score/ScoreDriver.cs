using Core.GameComposition;
using NaughtyAttributes;
using ScriptableObjects;
using UnityEngine;

namespace Core.Score
{
	public class ScoreDriver : MonoBehaviour
	{
		[BoxGroup("Dependencies")]
		[SerializeField]
		private ScoreSO _score;

		[BoxGroup("Dependencies")]
		[SerializeField]
		private GameCompositionRoot _gameCompositionRoot;

		private IScoreService _scoreService;

		public int Score => _score.Value;

		private void Awake()
		{
			_scoreService = new ScoreService(_gameCompositionRoot.EventBus);
		}

		private void OnDestroy()
		{
			_scoreService?.Dispose();
		}
	}
}
