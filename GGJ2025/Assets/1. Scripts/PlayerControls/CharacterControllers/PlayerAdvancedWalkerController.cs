using System;
using CMF;
using PlayerControls.Enums;
using PlayerControls.Interfaces;
using UnityEngine;

namespace PlayerControls.CharacterControllers
{
	public class PlayerAdvancedWalkerController : AdvancedWalkerController
	{
		[Header("Speed")]
		[SerializeField]
		private float walkSpeed = 2.5f;

		[SerializeField]
		private float jogSpeed = 5f;

		[SerializeField]
		private float runSpeed = 8f;

		[SerializeField]
		private float dodgeSpeed = 10f;

		private IMovementSpeedInputHandler movementSpeedInputHandler;

		protected override void Setup()
		{
			movementSpeedInputHandler = GetComponent<IMovementSpeedInputHandler>();
		}

		protected override Vector3 CalculateMovementDirection()
		{
			if (ReferenceEquals(characterInput, null))
			{
				return Vector3.zero;
			}

			Vector2 inputDirection = characterInput.GetInputMovementDirection(out bool isMoving);

			if (!isMoving)
			{
				return Vector3.zero;
			}

			Vector3 movementDirection = Vector3.zero;

			//If no camera transform has been assigned, use the character's transform axes to calculate the movement direction;
			if (cameraTransform == null)
			{
				movementDirection += cachedTransform.forward * inputDirection.y;
				movementDirection += cachedTransform.right * inputDirection.x;
			}
			else
			{
				//If a camera transform has been assigned, use the assigned transform's axes for movement direction;
				//Project movement direction so movement stays parallel to the ground;
				movementDirection += Vector3.ProjectOnPlane(cameraTransform.forward, cachedTransform.up).normalized * inputDirection.y;
				movementDirection += Vector3.ProjectOnPlane(cameraTransform.right, cachedTransform.up).normalized * inputDirection.x;
			}

			if (movementDirection.sqrMagnitude > 1)
			{
				movementDirection.Normalize();
			}

			return movementDirection;
		}

		protected override Vector3 CalculateMovementVelocity()
		{
			Vector3 movementDirection = CalculateMovementDirection();

			float speed = 0;

			if (IsDodging)
			{
				speed = dodgeSpeed;

				if (movementDirection == Vector3.zero)
				{
					movementDirection = poseTransform.forward;
				}
			}
			else
			{
				if (ReferenceEquals(movementSpeedInputHandler, null))
				{
					speed = movementSpeed;
				}
				else
				{
					speed = movementSpeedInputHandler.GetCurrentMovementType() switch
					{
						MovementType.Walk => walkSpeed,
						MovementType.Jog => jogSpeed,
						MovementType.Run => runSpeed,
						_ => 0,
					};
				}
			}

			Vector3 velocity = movementDirection * speed;

			return velocity;
		}
	}
}