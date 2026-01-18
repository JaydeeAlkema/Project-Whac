namespace Core.Random
{
	public sealed class SystemRandomProvider : IRandomProvider
	{
		private readonly System.Random _random;

		public SystemRandomProvider(int seed = 0)
		{
			_random = seed == 0 ? new() : new System.Random(seed);
		}

		public int Range(int min, int max)
		{
			return _random.Next(min, max);
		}
	}
}
