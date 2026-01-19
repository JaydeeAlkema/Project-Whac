namespace Core.Time
{
	public sealed class TimeProvider : ITimeProvider
	{
		public float DeltaTime => UnityEngine.Time.deltaTime;
	}
}
