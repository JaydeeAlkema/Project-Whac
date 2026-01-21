using System.Collections.Generic;
using Core.Score;

namespace UI.HighScores
{
	public sealed class HighScoresViewModel
	{
		public IReadOnlyList<ScoreEntry> Entries { get; }

		public HighScoresViewModel(IReadOnlyList<ScoreEntry> entries)
		{
			Entries = entries;
		}
	}
}
