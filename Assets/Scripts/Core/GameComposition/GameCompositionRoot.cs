using Core.Score;
using Core.Timer;
using UnityEngine;
using Utils;

namespace Core.GameComposition
{
	public class GameCompositionRoot : MonoBehaviour
	{
		public IEventBus EventBus { get; private set; }
		public IScoreService ScoreService { get; private set; }
		public ITimerService TimerService { get; private set; }

		private void Awake()
		{
			EventBus = new EventBus();
			ScoreService = new ScoreService(EventBus);
			TimerService = new TimerService(EventBus);
		}

		private void OnDestroy()
		{
			ScoreService?.Dispose();
			TimerService?.Dispose();
			EventBus.Clear();
		}
	}
}
