using Core.Score;
using Core.Timer;
using EventArgs.GameLoop;
using UnityEngine;
using Utils;

namespace Core.GameComposition
{
	/// <summary>
	///     The composition root of the game, responsible for initializing and providing access to core services.
	///     It felt a bit better than having a singleton at the ready for everything to use. Therefore, I chose this approach.
	///     Is it fool-proof? Well, in the context of this game, yes! but will I approach a similar way of making sure
	///     core services are accessible for other systems? Maybe.
	/// </summary>
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
