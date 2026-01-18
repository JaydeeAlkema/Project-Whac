using Core.MoleLogic.Mole;

namespace EventArgs
{
	public readonly struct MoleOnHitEventArgs
	{
		public readonly IMole Mole;
		public readonly int Score;

		public MoleOnHitEventArgs(IMole mole, int score)
		{
			Mole = mole;
			Score = score;
		}

		public override string ToString()
		{
			return $"MoleOnHitEventArgs {{ Mole = {Mole}, Score = {Score} }}";
		}
	}
}
