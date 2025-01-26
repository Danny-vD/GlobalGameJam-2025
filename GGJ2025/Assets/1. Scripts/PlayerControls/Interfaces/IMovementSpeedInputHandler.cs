using PlayerControls.Enums;

namespace PlayerControls.Interfaces
{
	public interface IMovementSpeedInputHandler
	{
		public MovementType GetCurrentMovementType();
	}
}