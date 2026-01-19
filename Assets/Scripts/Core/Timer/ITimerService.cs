using System;

namespace Core.Timer
{
	public interface ITimerService : IDisposable
	{
		event Action<float> OnTick;
		float Time { get; }
	}
}
