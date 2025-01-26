using UnityEngine;
using VDFramework.EventSystem;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private Vector3 triggerSize = new Vector3(3f, 3f, 3f);
    
    private BoxCollider triggerArea;

    void Awake()
    {
        // Add trigger collider if not present
        triggerArea = GetComponent<BoxCollider>();
        if (triggerArea == null)
        {
            triggerArea = gameObject.AddComponent<BoxCollider>();
            triggerArea.isTrigger = true;
            triggerArea.size = triggerSize;
        }

        // Validate respawn point
        if (respawnPoint == null)
        {
            Debug.LogWarning($"Checkpoint {gameObject.name} has no respawn point assigned!");
        }
    }

    public Vector3 GetRespawnPosition()
    {
        if (respawnPoint != null)
        {
            return respawnPoint.transform.position;
        }
        return transform.position; // Fallback to checkpoint position
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateCheckpoint();
        }
    }

    private void ActivateCheckpoint()
    {
        EventManager.RaiseEvent(new GameplayEvents.CheckpointActivateEvent(this.respawnPoint));
    }

    private void OnDrawGizmos()
    {
        // Draw trigger area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, triggerSize);
        
        // Draw respawn point and connection
        if (respawnPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(GetRespawnPosition(), 0.5f);
            Gizmos.DrawLine(transform.position, respawnPoint.transform.position);
        }
    }
}