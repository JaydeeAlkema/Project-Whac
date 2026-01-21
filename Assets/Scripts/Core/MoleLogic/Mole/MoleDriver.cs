using Core.GameComposition;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.MoleLogic.Mole
{
	public class MoleDriver : MonoBehaviour
	{
		[BoxGroup("Dependencies")]
		[SerializeField] private GameCompositionRoot _gameCompositionRoot;

		public IMole Mole { get; private set; }

		public void Initialize()
		{
			Animator animator = GetComponent<Animator>();
			Mole = new Mole(animator, _gameCompositionRoot.EventBus);
		}
	}
}
