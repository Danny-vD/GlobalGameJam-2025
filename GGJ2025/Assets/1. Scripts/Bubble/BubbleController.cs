using UnityEngine;
using VDFramework.EventSystem;

public class BubbleController : MonoBehaviour
{

    [SerializeField]
    private Animator canvasAnimator;

    // Trigger - passes through
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.isTrigger && !other.CompareTag("Environment"))
        {
            return;
        }

        EventManager.RaiseEvent(new GameplayEvents.PlayerFailedEvent(GameplayEvents.Enums.CauseOfFailure.BubblePopped));
    }

    private void OnParticleCollision(GameObject other)
    {
        EventManager.RaiseEvent(new GameplayEvents.PlayerFailedEvent(GameplayEvents.Enums.CauseOfFailure.BubblePopped));
    }

    private void OnParticleTrigger()
    {
        Debug.Log("AHHHHH");
    }
}