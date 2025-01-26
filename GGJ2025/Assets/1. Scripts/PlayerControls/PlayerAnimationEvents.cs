using CMF;
using VDFramework;

namespace PlayerControls
{
	public class PlayerAnimationEvents : BetterMonoBehaviour
	{
		private Controller controller;
		
		private void Awake()
		{
			controller = GetComponentInParent<Controller>();
		}

		public void DodgeEnded()
		{
			controller.DodgeEnded();
		}
	}
}