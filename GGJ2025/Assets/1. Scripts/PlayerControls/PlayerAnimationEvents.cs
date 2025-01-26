using CMF;
using VDFramework;

namespace PlayerControls
{
	public class PlayerAnimationEvents : BetterMonoBehaviour
	{
		private Controller controller;
		private AudioControl audioControl;
		
		private void Awake()
		{
			controller = GetComponentInParent<Controller>();
			audioControl = GetComponentInParent<AudioControl>();
		}

		public void DodgeEnded()
		{
			controller.DodgeEnded();
		}
		
		public void PlayBubblePop()
		{
			audioControl.PlayBubblePop();
		}
		
		public void PlayFootstepSound()
		{
			audioControl.PlayFootstepSound(0);
		}

		public void PlayhaawSound()
		{
			audioControl.PlayHaaw();
		}

		public void PlayJumpSound()
		{
			audioControl.PlayJump();
		}
	}
}