using Core.GameComposition;
using UI.GameScreen.Score;
using UI.GameScreen.Timer;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.GameScreen
{
	public sealed class GameScreenDriver : MonoBehaviour
	{
		[SerializeField] private UIDocument _ui;
		[SerializeField] private GameCompositionRoot _compositionRoot;

		private ScoreScope _scoreScope;
		private TimerScope _timerScope;

		private void Start()
		{
			VisualElement root = _ui.rootVisualElement;
			_scoreScope = new(_compositionRoot.ScoreService, root);
			_timerScope = new(_compositionRoot.TimerService, root);
		}


		private void OnDestroy()
		{
			_scoreScope?.Dispose();
			_timerScope?.Dispose();
		}
	}
}
