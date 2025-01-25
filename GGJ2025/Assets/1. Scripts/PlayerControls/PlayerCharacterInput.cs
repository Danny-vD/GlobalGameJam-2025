using CMF;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControls
{
	public class PlayerCharacterInput : CharacterInput
	{
		[Header("Input")]
		[SerializeField]
		private InputActionReference movementDirectionInput;
		
		[SerializeField]
		private InputActionReference jumpInput;

		public override float GetHorizontalMovementInput()
		{
			return movementDirectionInput.action.ReadValue<Vector2>().x;
		}

		public override float GetVerticalMovementInput()
		{
			return movementDirectionInput.action.ReadValue<Vector2>().y;
		}

		public override bool IsJumpKeyPressed()
		{
			return jumpInput.action.IsPressed();
		}
	}
}