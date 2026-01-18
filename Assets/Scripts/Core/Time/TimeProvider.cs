namespace Core.Time
{
	public class TimeProvider : ITimeProvider
	{
		public float DeltaTime => UnityEngine.Time.deltaTime;
	}
}
