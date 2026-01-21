using System;
using UnityEngine.UIElements;

namespace UI.GameOver
{
	public sealed class GameOverScope : IDisposable
	{
		public event Action ContinueRequested;
		
		public string Username => _binder.Username;
		
		private readonly GameOverUIBinder _binder;

		public GameOverScope(VisualElement root)
		{
			_binder = new(root);

			_binder.ContinueClicked += OnContinueClicked;
		}

		private void OnContinueClicked()
		{
			ContinueRequested?.Invoke();
		}

		public void Dispose()
		{
			_binder.ContinueClicked -= OnContinueClicked;
			_binder.Dispose();
		}
	}
}
