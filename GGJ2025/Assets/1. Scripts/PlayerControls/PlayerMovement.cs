using PlayerControls.Enums;
using UnityEngine;
using VDFramework;

namespace PlayerControls
{
	public class PlayerMovement : BetterMonoBehaviour
	{
		[SerializeField]
		private Transform cameraTransform;
		
		[Header("Speed")]
		[SerializeField]
		private float walkSpeed = 2.5f;
		
		[SerializeField]
		private float jogSpeed = 5f;
		
		[SerializeField]
		private float runSpeed = 8f;
		
		private CharacterController characterController;

		private void Awake()
		{
			characterController = GetComponent<CharacterController>();
		}

		public void Move(MovementType movementType, Vector2 direction)
		{
			float speed = movementType switch
			{
				MovementType.Walk => walkSpeed,
				MovementType.Jog => jogSpeed,
				MovementType.Run => runSpeed,
				_ => 0,
			};

			characterController.Move(speed * Time.deltaTime * TransformDirectionToCameraLocal(direction));
		}

		private Vector3 TransformDirectionToCameraLocal(Vector2 direction)
		{
			if (cameraTransform == null)
			{
				return direction;
			}

			Vector3 cameraRight = cameraTransform.right;
			cameraRight.y = 0;
			cameraRight.Normalize();

			Vector3 cameraForward = cameraTransform.forward;
			cameraForward.y = 0;
			cameraForward.Normalize();
			
			Vector3 result = cameraRight * direction.x;
			result += cameraForward * direction.y;

			return result;
		}
	}
}