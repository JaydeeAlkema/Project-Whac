using System;

namespace Core.RandomProvider
{
	public class SystemRandomProvider : IRandomProvider
	{
		private readonly Random _random;

		public SystemRandomProvider(int seed = 0)
		{
			_random = seed == 0 ? new() : new Random(seed);
		}

		public int Range(int min, int max)
		{
			return _random.Next(min, max);
		}
	}
}
