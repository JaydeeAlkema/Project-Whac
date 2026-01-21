using System;

namespace Core.Score
{
	public interface IScoreService : IDisposable
	{
		public event Action<int> ScoreChanged;
		int Score { get; }
		ScoreEntry CreateEntry(string username);
	}
}
