using UnityEngine;
using Utils;

namespace Core.GameComposition
{
	public class GameCompositionRoot : MonoBehaviour
	{
		public IEventBus EventBus { get; private set; }

		private void Awake()
		{
			EventBus = new EventBus();
		}

		private void OnDestroy()
		{
			EventBus.Clear();
		}
	}
}
