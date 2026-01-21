using System.Collections.Generic;

namespace Core.Score
{
	public interface IScoreRepository
	{
		void Add(ScoreEntry scoreEntry);
		IReadOnlyList<ScoreEntry> GetAll();
	}
}
