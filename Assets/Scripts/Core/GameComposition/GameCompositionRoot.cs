using Core.Score;
using UnityEngine;
using Utils;

namespace Core.GameComposition
{
	public class GameCompositionRoot : MonoBehaviour
	{
		public IEventBus EventBus { get; private set; }
		public IScoreService ScoreService { get; private set; }

		private void Awake()
		{
			EventBus = new EventBus();
			ScoreService = new ScoreService(EventBus);
		}

		private void OnDestroy()
		{
			ScoreService?.Dispose();
			EventBus.Clear();
		}
	}
}
