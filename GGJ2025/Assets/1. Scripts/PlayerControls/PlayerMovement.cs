using System.Collections;
using PlayerControls.Enums;
using UnityEngine;
using UtilityPackage.Utility.MathUtil;
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

		[Header("Curve")]
		[SerializeField]
		private InterpolationAlongCurve reachTargetVelocityCurve;

		[SerializeField]
		private InterpolationAlongCurve stopMovingCurve;

		public Vector3 Velocity { get; set; } = Vector3.zero;

		public Vector3 TargetVelocity { get; set; } = Vector3.zero;

		public bool IsStopping => TargetVelocity == Vector3.zero;

		private CharacterController characterController;

		private Vector3 previousVelocity = Vector3.zero;

		private float curveTime;

		private Coroutine updateSpeedCoroutine;

		private void Awake()
		{
			characterController = GetComponent<CharacterController>();
		}

		private IEnumerator UpdateSpeed()
		{
			while (TargetVelocity != Vector3.zero || Velocity != Vector3.zero)
			{
				InterpolationAlongCurve currentCurve = IsStopping ? stopMovingCurve : reachTargetVelocityCurve;

				if (curveTime < currentCurve.MaxTime)
				{
					float lerpValue = currentCurve.EvaluateCurve(curveTime);
					curveTime += Time.deltaTime;

					Velocity = Vector3.Lerp(previousVelocity, TargetVelocity, lerpValue);
				}
				else
				{
					Velocity = TargetVelocity;
				}

				characterController.Move(Velocity * Time.deltaTime);
				Debug.Log(Velocity.magnitude);
				
				yield return null;
			}

			updateSpeedCoroutine = null;
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

			previousVelocity = Velocity;
			TargetVelocity   = speed * TransformDirectionToCameraLocal(direction);
			curveTime        = 0;

			updateSpeedCoroutine ??= StartCoroutine(UpdateSpeed());
		}

		public void StopMoving()
		{
			TargetVelocity = Vector3.zero;
			curveTime      = 0;
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