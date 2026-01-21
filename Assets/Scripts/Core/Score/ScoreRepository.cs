using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Core.Score
{
	public sealed class ScoreRepository : IScoreRepository
	{
		private const string FILE_NAME = "scores.json";

		private readonly string _path;
		private readonly List<ScoreEntry> _entries = new();

		public ScoreRepository()
		{
			_path = Path.Combine(Application.persistentDataPath, FILE_NAME);
			Load();
		}

		public void Add(ScoreEntry scoreEntry)
		{
			_entries.Add(scoreEntry);
			Save();
		}

		private void Save()
		{
			ScoreSaveData data = new()
			{
				_entries = _entries,
			};

			string json = JsonUtility.ToJson(data, true);
			File.WriteAllText(_path, json);
		}

		private void Load()
		{
			if (!File.Exists(_path))
			{
				_entries.Clear();
				return;
			}

			string json = File.ReadAllText(_path);
			ScoreSaveData data = JsonUtility.FromJson<ScoreSaveData>(json);

			if (data?._entries == null)
				return;

			_entries.AddRange(data._entries);
		}

		public IReadOnlyList<ScoreEntry> GetAll()
		{
			return _entries.OrderByDescending(e => e._score).ToList();
		}
	}
}
