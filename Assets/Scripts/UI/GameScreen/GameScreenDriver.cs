using Core.GameComposition;
using UI.GameScreen.Score;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.GameScreen
{
	public sealed class GameScreenDriver : MonoBehaviour
	{
		[SerializeField] private UIDocument _ui;
		[SerializeField] private GameCompositionRoot _compositionRoot;

		private ScoreScope _scoreScope;

		private void Start()
		{
			VisualElement root = _ui.rootVisualElement;

			Debug.Assert(root != null, "rootVisualElement is null");
			Debug.Assert(_ui != null, "_ui is null");
			Debug.Assert(_compositionRoot != null, "_compositionRoot is null");
			Debug.Assert(_compositionRoot.ScoreService != null, "ScoreService is null");

			_scoreScope = new(_compositionRoot.ScoreService, root);
		}


		private void OnDestroy()
		{
			_scoreScope.Dispose();
		}
	}
}
