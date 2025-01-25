using UnityEngine;

namespace PlayerControls.Interfaces
{
	public interface IMovementInputHandler
	{
		public Vector2 GetInputMovementDirection(out bool isMoving);
	}
}