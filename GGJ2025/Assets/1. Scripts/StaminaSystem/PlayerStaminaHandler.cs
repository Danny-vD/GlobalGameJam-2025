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
		private SerializableDictionary<float, GameObject> particleThresholds;

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
				SerializableKeyValuePair<float, GameObject> pair = particleThresholds[i];

				if (activatedObject)
				{
					pair.Value?.SetActive(false);
					continue;
				}

				float threshold = pair.Key;

				if (currentStamina <= threshold)
				{
					pair.Value?.SetActive(true);
					activatedObject = true;
				}
				else
				{
					pair.Value?.SetActive(false);
				}
			}
		}

		private void Fail(bool canKill)
		{
			if (canKill)
			{
				foreach (KeyValuePair<float, GameObject> pair in particleThresholds)
				{
					pair.Value?.SetActive(false);
				}

				EventManager.RaiseEvent(new PlayerFailedEvent(CauseOfFailure.StaminaDrained));
			}
		}
	}
}