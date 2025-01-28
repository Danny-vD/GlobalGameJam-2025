using GameplayEvents.Enums;
using VDFramework.EventSystem;

namespace GameplayEvents
{
	public class PlayerFailedEvent : VDEvent<PlayerFailedEvent>
	{
		public readonly CauseOfFailure CauseOfFailure;

		public PlayerFailedEvent(CauseOfFailure causeOfFailure)
		{
			CauseOfFailure = causeOfFailure;
		}
	}
}