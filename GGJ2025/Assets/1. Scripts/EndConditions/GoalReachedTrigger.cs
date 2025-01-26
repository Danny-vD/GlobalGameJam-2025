using GameplayEvents;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace EndConditions
{
	public class GoalReachedTrigger : BetterMonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				EventManager.RaiseEvent(new PlayerSucceededEvent());
			}
		}
	}
}