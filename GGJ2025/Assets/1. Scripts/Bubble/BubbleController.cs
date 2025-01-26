using UnityEngine;
using VDFramework.EventSystem;

public class BubbleController : MonoBehaviour
{
    // Trigger - passes through
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.isTrigger)
        {
            return;
        }

        EventManager.RaiseEvent(new GameplayEvents.PlayerFailedEvent(GameplayEvents.Enums.CauseOfFailure.BubblePopped));
    }
}