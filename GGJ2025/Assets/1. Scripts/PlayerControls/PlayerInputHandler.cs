﻿using System.Collections;
using PlayerControls.Enums;
using UnityEngine;
using UnityEngine.InputSystem;
using VDFramework;

namespace PlayerControls
{
	[RequireComponent(typeof(PlayerMovement))]
	public class PlayerInputHandler : BetterMonoBehaviour
	{
		[SerializeField]
		private InputActionReference movementDirectionInput;

		[SerializeField]
		private InputActionReference walkInput;

		[SerializeField]
		private InputActionReference runInput;

		private bool shouldRun;
		private bool shouldWalk;

		private PlayerMovement playerMovement;

		private Coroutine moveCoroutine;

		private void Awake()
		{
			playerMovement = GetComponent<PlayerMovement>();
		}

		private void OnEnable()
		{
			movementDirectionInput.action.performed += StartMoving;
			movementDirectionInput.action.canceled  += StopMoving;

			runInput.action.performed += StartRunning;
			runInput.action.canceled  += StopRunning;

			walkInput.action.performed += StartWalking;
			walkInput.action.canceled  += StopWalking;
		}

		private void OnDisable()
		{
			movementDirectionInput.action.performed -= StartMoving;
			movementDirectionInput.action.canceled  -= StopMoving;

			runInput.action.performed -= StartRunning;
			runInput.action.canceled  -= StopRunning;

			walkInput.action.performed -= StartWalking;
			walkInput.action.canceled  -= StopWalking;
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

		private void StartMoving(InputAction.CallbackContext obj)
		{
			moveCoroutine ??= StartCoroutine(MoveCoroutine());
		}

		private void StopMoving(InputAction.CallbackContext obj)
		{
			if (moveCoroutine != null)
			{
				StopCoroutine(moveCoroutine);
				moveCoroutine = null;
			}
		}

		private IEnumerator MoveCoroutine()
		{
			while (true)
			{
				playerMovement.Move(GetCurrentMovementType(), GetInputMovementDirection(out _));
				yield return null;
			}
		}

		private void StartRunning(InputAction.CallbackContext obj)
		{
			shouldRun = true;
		}

		private void StopRunning(InputAction.CallbackContext obj)
		{
			shouldRun = false;
		}

		private void StartWalking(InputAction.CallbackContext obj)
		{
			shouldWalk = true;
		}

		private void StopWalking(InputAction.CallbackContext obj)
		{
			shouldWalk = false;
		}

		private Vector2 GetInputMovementDirection(out bool isMoving)
		{
			isMoving = movementDirectionInput.action.IsInProgress();

			return movementDirectionInput.action.ReadValue<Vector2>();
		}
	}
}