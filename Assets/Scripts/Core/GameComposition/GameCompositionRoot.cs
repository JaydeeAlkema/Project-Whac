using Core.Score;
using Core.Timer;
using EventArgs.GameLoop;
using UnityEngine;
using Utils;

namespace Core.GameComposition
{
	public class GameCompositionRoot : MonoBehaviour
	{
		public IEventBus EventBus { get; private set; }

		public IScoreService ScoreService { get; private set; }
		public IScoreRepository ScoreRepository { get; private set; }

		public ITimerService TimerService { get; private set; }

		private void Awake()
		{
			EventBus = new EventBus();

			ScoreService = new ScoreService(EventBus);
			ScoreRepository = new ScoreRepository();

			TimerService = new TimerService(EventBus);

			Application.targetFrameRate = 60;

			EventBus.PublishSticky(new GameOnStartEventArgs());
		}

		private void OnDestroy()
		{
			ScoreService?.Dispose();
			TimerService?.Dispose();
			EventBus.Clear();
		}
	}
}
