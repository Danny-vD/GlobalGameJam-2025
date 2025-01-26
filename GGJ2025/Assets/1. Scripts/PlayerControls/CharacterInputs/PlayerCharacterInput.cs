using CMF;
using PlayerControls.Enums;
using PlayerControls.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControls.CharacterInputs
{
	public class PlayerCharacterInput : CharacterInput, IMovementSpeedInputHandler
	{
		[Header("Input")]
		[SerializeField]
		private InputActionReference movementDirectionInput;

		[SerializeField]
		private InputActionReference walkInput;

		[SerializeField]
		private InputActionReference runInput;

		[SerializeField]
		private InputActionReference jumpInput;
		
		[SerializeField]
		private InputActionReference dodgeInput;

		[Header("Settings")]
		[SerializeField]
		private bool runIsToggle;

		[SerializeField]
		private bool walkIsToggle;

		private bool shouldRun;
		private bool shouldWalk;

		private void OnEnable() //
		{
			runInput.action.performed += StartRunning;
			runInput.action.canceled  += StopRunning;

			walkInput.action.performed += StartWalking;
			walkInput.action.canceled  += StopWalking;
		}

		private void OnDisable()
		{
			runInput.action.performed -= StartRunning;
			runInput.action.canceled  -= StopRunning;

			walkInput.action.performed -= StartWalking;
			walkInput.action.canceled  -= StopWalking;
		}

		public override float GetHorizontalMovementInput()
		{
			return movementDirectionInput.action.ReadValue<Vector2>().x;
		}

		public override float GetVerticalMovementInput()
		{
			return movementDirectionInput.action.ReadValue<Vector2>().y;
		}
		
		public override Vector2 GetInputMovementDirection(out bool isMoving)
		{
			isMoving = movementDirectionInput.action.IsInProgress();

			return movementDirectionInput.action.ReadValue<Vector2>();
		}

		public override bool IsJumpKeyPressed()
		{
			return jumpInput.action.IsPressed();
		}

		public override bool IsDodgeKeyPressed()
		{
			return dodgeInput.action.IsPressed();
		}

		public MovementType GetCurrentMovementType()
		{
			if (shouldRun)
			{
				return MovementType.Run;
			}

			if (shouldWalk)
			{
				return MovementType.Walk;
			}

			return MovementType.Jog;
		}

		private void StartRunning(InputAction.CallbackContext obj)
		{
			if (runIsToggle)
			{
				shouldRun = !shouldRun;
			}
			else
			{
				shouldRun = true;
			}
		}

		private void StopRunning(InputAction.CallbackContext obj)
		{
			if (!runIsToggle)
			{
				shouldRun = false;
			}
		}

		private void StartWalking(InputAction.CallbackContext obj)
		{
			if (walkIsToggle)
			{
				shouldWalk = !shouldWalk;
			}
			else
			{
				shouldWalk = true;
			}
		}

		private void StopWalking(InputAction.CallbackContext obj)
		{
			if (!walkIsToggle)
			{
				shouldWalk = false;
			}
		}
	}
}