using EventArgs;
using UnityEngine;
using Utils;

namespace Core.MoleLogic.Mole
{
	public sealed class Mole : IMole
	{
		private readonly IEventBus _eventBus;
		private readonly int _scorePerHit;

		public bool IsVisible { get; private set; }

		// Animator is optional for tests; can be null in pure C#
		private readonly Animator _animator;
		private readonly int _spawnHash = Animator.StringToHash("Spawn");
		private readonly int _hideHash = Animator.StringToHash("Hide");
		private readonly int _hitHash = Animator.StringToHash("Hit");

		public Mole(Animator animator, IEventBus eventBus, int scorePerHit = 100)
		{
			_animator = animator;
			_eventBus = eventBus;
			_scorePerHit = scorePerHit;
			Hide();
		}

		public void Show()
		{
			IsVisible = true;
			_animator?.Play(_spawnHash);
		}

		public void Hide()
		{
			IsVisible = false;
			_animator?.Play(_hideHash);
		}

		public void Hit()
		{
			if (!IsVisible) return;

			IsVisible = false;
			_animator?.Play(_hitHash);

			MoleOnHitEventArgs args = new(this, _scorePerHit);
			_eventBus.PublishSticky(args);
		}
	}
}
