using System;
using EventArgs;
using Utils;

namespace Core.Score
{
	public sealed class ScoreService : IScoreService
	{
		public event Action<int> ScoreChanged;
		public int Score { get; private set; }
		
		private readonly IDisposable _subscription;

		public ScoreService(IEventBus bus)
		{
			_subscription = bus.Subscribe<MoleOnHitEventArgs>(OnMoleHit);
		}

		private void OnMoleHit(MoleOnHitEventArgs args)
		{
			Score += args.Score;
			ScoreChanged?.Invoke(Score);
		}

		public void Dispose()
		{
			_subscription.Dispose();
		}
	}
}
