using System;
using UnityEngine.UIElements;

namespace UI.GameOver
{
	public sealed class GameOverUIBinder : IDisposable
	{
		private readonly TextField _usernameField;
		private readonly Button _continueButton;

		public event Action ContinueClicked;

		public string Username => _usernameField.text;

		public GameOverUIBinder(VisualElement root)
		{
			_usernameField = root.Q<TextField>("usernameTextField");
			_continueButton = root.Q<Button>("continue");

			_continueButton.clicked += OnContinueButtonClicked;
		}

		private void OnContinueButtonClicked()
		{
			ContinueClicked?.Invoke();
		}

		public void Dispose()
		{
			_continueButton.clicked -= OnContinueButtonClicked;
		}
	}
}
