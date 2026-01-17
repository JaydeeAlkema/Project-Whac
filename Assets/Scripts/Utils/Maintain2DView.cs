using UnityEngine;

namespace Utils
{
	[ExecuteAlways]
	public class Maintain2DView : MonoBehaviour
	{
		public float _targetVerticalSize = 5f;    // half-height you designed for
		public float _referenceAspect = 9f / 16f; // target aspect like 9x16

		private Camera _camera;

		private void Awake()
		{
			TryCacheCamera();
		}

		private void Update()
		{
			float currentAspect = (float)Screen.width / Screen.height;
			if (!_camera)
				TryCacheCamera();

			_camera.orthographicSize = _targetVerticalSize * (_referenceAspect / currentAspect);
		}

		private void TryCacheCamera()
		{
			if (_camera)
				return;

			_camera = GetComponent<Camera>();
		}
	}
}
