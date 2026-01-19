using Core.GameComposition;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.MoleLogic.Mole
{
	public class MoleDriver : MonoBehaviour, IPointerClickHandler
	{
		[BoxGroup("Dependencies")]
		[SerializeField] private GameCompositionRoot _gameCompositionRoot;

		public IMole Mole { get; private set; }

		private void Start()
		{
			Animator animator = GetComponent<Animator>();
			Mole = new Mole(animator, _gameCompositionRoot.EventBus);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			Mole.Hit();
		}
	}
}
