using System;
using CMF;
using GameplayEvents;
using GameplayEvents.Enums;
using PlayerControls.Enums;
using PlayerControls.Interfaces;
using UnityEngine;
using UtilityPackage.Attributes;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.Utility.TimerUtil;
using VDFramework.Utility.TimerUtil.TimerHandles;

namespace StaminaSystem
{
	[DefaultExecutionOrder(-1)] // Has to happen before the controller handles movement
	public class StaminaManager : BetterMonoBehaviour
	{
		public event Action OnStaminaFull = delegate { };

		/// <summary>
		/// Invoked when the stamina changes.<br/>
		/// arguments: currentStamina | normalizedStamina | delta 
		/// </summary>
		public event Action<float, float, float> OnStaminaChanged = delegate { };

		/// <summary>
		/// Invoked when the stamina reaches 0%.<br/>
		/// argument: canKill
		/// </summary>
		public event Action<bool> OnStaminaDepleted = delegate { };

		[Header("Base settings")]
		[SerializeField]
		private float maximumStamina = 100;

		[SerializeField]
		private float staminaRegenerateCooldown = 2;

		[Header("Stamina values")]
		[SerializeField]
		private float regenerationRate = 25f;

		[Space(2)]
		[SerializeField]
		private float runCost = 25f;

		[SerializeField]
		private float jumpCost = 25f;

		[SerializeField]
		private float dodgeCost = 25f;

		private float currentStamina;

		private Controller controller;
		private CharacterInput characterInput;
		private IMovementSpeedInputHandler movementSpeedInputHandler;

		private TimerHandle regeneratingCooldownTimer;
		private bool allowRegenerating = true;

		private bool isJumping = false;
		private bool isDodging = false;

		private void Awake()
		{
			controller                = GetComponent<Controller>();
			characterInput            = GetComponent<CharacterInput>();
			movementSpeedInputHandler = GetComponent<IMovementSpeedInputHandler>();
		}

		private void Start()
		{
			currentStamina = maximumStamina;
		}

		private void OnEnable()
		{
			controller.OnJump += OnJump;
			controller.OnLand += OnLand;

			controller.OnDodge    += OnDodge;
			controller.OnDodgeEnd += OnDodgeEnded;
		}

		private void OnDisable()
		{
			controller.OnJump -= OnJump;
			controller.OnLand -= OnLand;

			controller.OnDodge    -= OnDodge;
			controller.OnDodgeEnd -= OnDodgeEnded;
		}

		private void FixedUpdate()
		{
			_ = characterInput.GetInputMovementDirection(out bool isMoving);

			if (isMoving && movementSpeedInputHandler.GetCurrentMovementType() == MovementType.Run && controller.IsGrounded() && !isDodging)
			{
				DrainStamina(runCost * Time.fixedDeltaTime, true);
			}
			else if (allowRegenerating && !isJumping && !isDodging)
			{
				RegenerateStamina(regenerationRate * Time.fixedDeltaTime);
			}
		}

		public float GetStamina()
		{
			return currentStamina;
		}

		public float GetStaminaNormalized()
		{
			return currentStamina / maximumStamina;
		}

		private void RegenerateStamina(float delta)
		{
			if (currentStamina >= maximumStamina)
			{
				return;
			}

			ChangeStamina(delta, false);
		}

		private void DrainStamina(float delta, bool canKill)
		{
			ChangeStamina(-delta, canKill);
		}

		private void ChangeStamina(float delta, bool canKill)
		{
			canKill = canKill || currentStamina <= 0;

			currentStamina += delta;

			OnStaminaChanged.Invoke(GetStamina(), GetStaminaNormalized(), delta);

			if (currentStamina <= 0)
			{
				OnStaminaDepleted.Invoke(canKill);

				if (canKill)
				{
					allowRegenerating = false;
					enabled           = false; // Disable this script
				}
				else
				{
					currentStamina = 0;
					StartStaminaRegenerateCooldownTimer();
				}
			}
			else if (currentStamina >= maximumStamina)
			{
				currentStamina = maximumStamina;
				OnStaminaFull.Invoke();
			}
		}

		private void OnJump(Vector3 vector3)
		{
			isJumping = true;

			DrainStamina(jumpCost, false);
		}

		private void OnLand(Vector3 vector3)
		{
			isJumping = false;
		}

		private void OnDodge()
		{
			isDodging = true;

			DrainStamina(dodgeCost, false);
		}

		private void OnDodgeEnded()
		{
			isDodging = false;
		}

		private void StartStaminaRegenerateCooldownTimer()
		{
			allowRegenerating = false;

			regeneratingCooldownTimer ??= TimerManager.StartNewTimer(staminaRegenerateCooldown, StaminaRegenerateTimerExpired);
		}

		private void StaminaRegenerateTimerExpired()
		{
			regeneratingCooldownTimer = null;

			allowRegenerating = true;
		}
	}
}