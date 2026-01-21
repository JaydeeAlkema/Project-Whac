using System;

namespace Core.Score
{
	[Serializable]
	public struct ScoreEntry
	{
		public string _username;
		public int _score;

		public override string ToString()
		{
			return $"{_score}: {_username}";
		}
	}
}
