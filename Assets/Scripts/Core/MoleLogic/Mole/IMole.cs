namespace Core.MoleLogic.Mole
{
	public interface IMole
	{
		bool IsVisible { get; }
		void Show();
		void Hide();
		void Hit();
	}
}
