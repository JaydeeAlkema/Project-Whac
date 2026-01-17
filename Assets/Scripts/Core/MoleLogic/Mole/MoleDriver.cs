using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.MoleLogic.Mole
{
	public class MoleDriver : MonoBehaviour, IPointerClickHandler
	{
		public IMole Mole { get; private set; }

		private void Awake()
		{
			Animator animator = GetComponent<Animator>();
			Mole = new Mole(animator);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			Mole.Hit();
		}
	}
}
