using System.Collections.Generic;
using GameplayEvents;
using GameplayEvents.Enums;
using SerializableDictionaryPackage.SerializableDictionary;
using SerializableDictionaryPackage.Structs;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace StaminaSystem
{
	[RequireComponent(typeof(StaminaManager))]
	public class PlayerStaminaHandler : BetterMonoBehaviour
	{
		[SerializeField]
		private SerializableDictionary<float, ParticleSystem> particleThresholds;

		private StaminaManager staminaManager;

		private void Awake()
		{
			staminaManager = GetComponent<StaminaManager>();
		}

		private void OnEnable()
		{
			staminaManager.OnStaminaChanged  += OnStaminaChanged;
			staminaManager.OnStaminaDepleted += Fail;
		}

		private void OnDisable()
		{
			staminaManager.OnStaminaChanged  -= OnStaminaChanged;
			staminaManager.OnStaminaDepleted -= Fail;
		}

		private void OnStaminaChanged(float currentStamina, float normalizedStamina, float delta)
		{
			bool activatedObject = false;

			for (int i = particleThresholds.Count - 1; i >= 0; i--)
			{
				SerializableKeyValuePair<float, ParticleSystem> pair = particleThresholds[i];
				ParticleSystem.EmissionModule emissionModule = pair.Value.emission;

				if (activatedObject)
				{
					emissionModule.enabled = false;
					continue;
				}

				float threshold = pair.Key;

				if (currentStamina <= threshold)
				{
					emissionModule.enabled = true;

					activatedObject = true;
				}
				else
				{
					emissionModule.enabled = false;
				}
			}
		}

		private void Fail(bool canKill)
		{
			if (canKill)
			{
				foreach (KeyValuePair<float, ParticleSystem> pair in particleThresholds)
				{
					ParticleSystem.EmissionModule emissionModule = pair.Value.emission;
					emissionModule.enabled = false;
				}

				EventManager.RaiseEvent(new PlayerFailedEvent(CauseOfFailure.StaminaDrained));
			}
		}
	}
}