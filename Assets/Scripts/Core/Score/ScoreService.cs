using System;
using EventArgs;
using Utils;

namespace Core.Score
{
	public sealed class ScoreService : IScoreService
	{
		private readonly IDisposable _subscription;

		public int Score { get; private set; }

		public ScoreService(IEventBus bus)
		{
			_subscription = bus.Subscribe<MoleOnHitEventArgs>(e => Score += e.Score);
		}

		public void Dispose()
		{
			_subscription.Dispose();
		}
	}
}
