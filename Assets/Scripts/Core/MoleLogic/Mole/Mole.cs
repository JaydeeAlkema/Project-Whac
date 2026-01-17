using EventArgs;
using UnityEngine;
using Utils;

namespace Core.MoleLogic.Mole
{
	public class Mole : IMole
	{
		private readonly int _spawnAnimationHash = Animator.StringToHash("Spawn");
		private readonly int _hideAnimationHash = Animator.StringToHash("Hide");
		private readonly int _hitAnimationHash = Animator.StringToHash("Hit");

		private readonly Animator _animator;

		public bool IsVisible { get; private set; }

		public Mole(Animator animator)
		{
			_animator = animator;
			Hide();
		}

		public void Show()
		{
			IsVisible = true;
			_animator.Play(_spawnAnimationHash);
		}

		public void Hide()
		{
			IsVisible = false;
			_animator.Play(_hideAnimationHash);
		}

		public void Hit()
		{
			if (!IsVisible)
				return;

			IsVisible = false;
			_animator.Play(_hitAnimationHash);
			EventBus.PublishSticky(new MoleOnHitEventArgs { Mole = this, });
		}
	}
}
