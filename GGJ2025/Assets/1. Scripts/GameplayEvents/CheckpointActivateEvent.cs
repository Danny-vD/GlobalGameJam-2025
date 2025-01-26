using UnityEngine;
using VDFramework.EventSystem;

namespace GameplayEvents
{
	public class CheckpointActivateEvent : VDEvent<CheckpointActivateEvent>
	{
        public GameObject checkpoint;

        public CheckpointActivateEvent(GameObject checkpoint)
        {
            this.checkpoint = checkpoint;
        }
	}
}