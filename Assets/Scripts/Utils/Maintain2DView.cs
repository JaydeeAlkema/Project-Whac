using UnityEngine;

namespace Utils
{
	[ExecuteAlways]
	public class Maintain2DView : MonoBehaviour
	{
		public float _targetVerticalSize = 5f;
		public float _referenceAspect = 9f / 16f;

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
