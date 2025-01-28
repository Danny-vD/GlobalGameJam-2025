using CMF;
using GameplayEvents;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace EndConditions
{
	[RequireComponent(typeof(Controller))]
	public class DisableMovementOnFail : BetterMonoBehaviour
	{
		private Controller controller;
		
		private void Awake()
		{
			controller = GetComponent<Controller>();
		}

		private void OnEnable()
		{
			EventManager.AddListener<PlayerFailedEvent>(DisableMovement);
		}

		private void OnDisable()
		{
			EventManager.RemoveListener<PlayerFailedEvent>(DisableMovement);
		}

		private void DisableMovement()
		{
			controller.enabled = false;
		}
	}
}