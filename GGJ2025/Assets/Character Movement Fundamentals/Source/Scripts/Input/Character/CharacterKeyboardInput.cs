using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
	//This character movement input class is an example of how to get input from a keyboard to control the character;
	public class CharacterKeyboardInput : CharacterInput
	{
		public string horizontalInputAxis = "Horizontal";
		public string verticalInputAxis = "Vertical";
		public KeyCode jumpKey = KeyCode.Space;
		public KeyCode dodgeKey = KeyCode.LeftAlt;

		//If this is enabled, Unity's internal input smoothing is bypassed;
		public bool useRawInput = true;

		public override float GetHorizontalMovementInput()
		{
			if (useRawInput)
				return Input.GetAxisRaw(horizontalInputAxis);
			else
				return Input.GetAxis(horizontalInputAxis);
		}

		public override float GetVerticalMovementInput()
		{
			if (useRawInput)
				return Input.GetAxisRaw(verticalInputAxis);
			else
				return Input.GetAxis(verticalInputAxis);
		}

		public override Vector2 GetInputMovementDirection(out bool isMoving)
		{
			float horizontalInput;
			float verticalInput;

			if (useRawInput)
			{
				horizontalInput = Input.GetAxisRaw(horizontalInputAxis);
				verticalInput   = Input.GetAxisRaw(verticalInputAxis);
			}
			else
			{
				horizontalInput = Input.GetAxis(horizontalInputAxis);
				verticalInput   = Input.GetAxis(verticalInputAxis);
			}

			isMoving = horizontalInput > 0 || verticalInput > 0;

			return new Vector2(horizontalInput, verticalInput);
		}

		public override bool IsJumpKeyPressed()
		{
			return Input.GetKey(jumpKey);
		}

		public override bool IsDodgeKeyPressed()
		{
			return Input.GetKey(dodgeKey);
		}
	}
}