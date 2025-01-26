using GameplayEvents;
using GameplayEvents.Enums;
using SerializableDictionaryPackage.SerializableDictionary;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace EndConditions
{
	[RequireComponent(typeof(Animator))]
	public class FailAnimationManager : BetterMonoBehaviour
	{
		[SerializeField]
		private SerializableEnumDictionary<CauseOfFailure, string> triggerPerFailCondition;

		private Animator animator;

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

		private void OnEnable()
		{
			EventManager.AddListener<PlayerFailedEvent>(PlayAnimation);
		}

		private void OnDisable()
		{
			EventManager.RemoveListener<PlayerFailedEvent>(PlayAnimation);
		}

		private void PlayAnimation(PlayerFailedEvent playerFailedEvent)
		{
			CauseOfFailure causeOfFailure = playerFailedEvent.CauseOfFailure;

			if (causeOfFailure == CauseOfFailure.None)
			{
				return;
			}
			
			string trigger = triggerPerFailCondition[causeOfFailure];
			
			animator.SetTrigger(trigger);
		}
	}
}