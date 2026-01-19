using System;

namespace Core.Timer
{
	public interface ITimerService : IDisposable
	{
		event Action<float> OnTick;
		event Action<float> OnStarted;
		event Action<float> OnEnded;
		float Time { get; }
	}
}
