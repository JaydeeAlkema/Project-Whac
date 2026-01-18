using System;

namespace Core.Score
{
	public interface IScoreService : IDisposable
	{
		int Score { get; }
	}
}
