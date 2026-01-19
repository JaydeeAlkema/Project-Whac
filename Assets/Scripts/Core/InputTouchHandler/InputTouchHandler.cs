using Core.MoleLogic.Mole;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputTouchHandler
{
	public sealed class InputTouchHandler : MonoBehaviour
	{
		[SerializeField] private Camera _camera;

		private InputAction _primaryPress;

		private void Awake()
		{
			if (_camera == null)
				_camera = Camera.main;

			// Create action in code (or inject from asset)
			_primaryPress = new(
				"PrimaryPress",
				InputActionType.Button,
				"<Pointer>/press"
			);

			_primaryPress.performed += OnPrimaryPress;
		}

		private void OnEnable()
		{
			_primaryPress.Enable();
		}

		private void OnDisable()
		{
			_primaryPress.Disable();
		}

		private void OnDestroy()
		{
			_primaryPress.performed -= OnPrimaryPress;
		}

		private void OnPrimaryPress(InputAction.CallbackContext context)
		{
			Vector2 screenPosition = Pointer.current.position.ReadValue();
			TryHitAt(screenPosition);
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
