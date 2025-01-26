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

		private IMovementSpeedInputHandler movementSpeedInputHandler;
		private IMovementInputHandler movementInputHandler;

		protected override void Setup()
		{
			movementSpeedInputHandler = GetComponent<IMovementSpeedInputHandler>();
			movementInputHandler      = GetComponent<IMovementInputHandler>();
		}

		protected override Vector3 CalculateMovementDirection()
		{
			if (ReferenceEquals(movementInputHandler, null))
			{
				return Vector3.zero;
			}
			
			Vector2 inputDirection = movementInputHandler.GetInputMovementDirection(out bool isMoving);

			if (!isMoving)
			{
				return Vector3.zero;
			}

			Vector3 movementDirection = Vector3.zero;

			//If no camera transform has been assigned, use the character's transform axes to calculate the movement direction;
			if (cameraTransform == null)
			{
				movementDirection += tr.forward * inputDirection.y;
				movementDirection += tr.right * inputDirection.x;
			}
			else
			{
				//If a camera transform has been assigned, use the assigned transform's axes for movement direction;
				//Project movement direction so movement stays parallel to the ground;
				movementDirection += Vector3.ProjectOnPlane(cameraTransform.forward, tr.up).normalized * inputDirection.y;
				movementDirection += Vector3.ProjectOnPlane(cameraTransform.right, tr.up).normalized * inputDirection.x;
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

			Vector3 velocity = movementDirection * speed;

			return velocity;
		}
	}
}