using VDFramework.EventSystem;

namespace GameplayEvents
{
	public class CheckpointActivateEvent : VDEvent<CheckpointActivateEvent>
	{
        public Checkpoint checkpoint;

        public CheckpointActivateEvent(Checkpoint checkpoint)
        {
            this.checkpoint = checkpoint;
        }
	}
}