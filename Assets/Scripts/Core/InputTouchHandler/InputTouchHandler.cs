using Core.MoleLogic.Mole;
using UnityEngine;

namespace Core.InputTouchHandler
{
	public class InputTouchHandler : MonoBehaviour
	{
		private Camera _camera;

		private void Awake()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			// On mobile, use touch
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began)
					TryHitAt(touch.position);
			}

			// Also support mouse click for editor testing
			if (Input.GetMouseButtonDown(0))
				TryHitAt(Input.mousePosition);
		}

		private void TryHitAt(Vector2 screenPosition)
		{
			Vector2 worldPoint = _camera.ScreenToWorldPoint(screenPosition);

			Collider2D hit = Physics2D.OverlapPoint(worldPoint);
			if (!hit)
				return;

			MoleDriver mole = hit.GetComponent<MoleDriver>();
			mole?.Mole.Hit();
		}
	}
}
