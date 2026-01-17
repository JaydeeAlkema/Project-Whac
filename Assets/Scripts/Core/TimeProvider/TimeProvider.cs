using UnityEngine;

namespace Core.TimeProvider
{
	public class TimeProvider : ITimeProvider
	{
		public float DeltaTime => Time.deltaTime;
	}
}
