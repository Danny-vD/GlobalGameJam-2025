using UnityEngine;
using System.Collections;
using VDFramework.EventSystem;

public class CheckpointManager : MonoBehaviour
{
    private Checkpoint activeCheckpoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnDelay = 1f;

    private void Awake()
    {
        EventManager.AddListener((GameplayEvents.CheckpointActivateEvent checkpointActivate) => SetActiveCheckpoint(checkpointActivate.checkpoint));
        EventManager.AddListener((GameplayEvents.PlayerFailedEvent playerFailed) => RespawnPlayer());
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("No player found in scene!");
        }
    }

    public void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        Debug.Log($"Checkpoint {checkpoint.name} activated!");
        activeCheckpoint = checkpoint;
    }

    public Checkpoint GetActiveCheckpoint()
    {
        return activeCheckpoint;
    }

    public Vector3 GetRespawnPosition()
    {
        return activeCheckpoint != null ? activeCheckpoint.GetRespawnPosition() : Vector3.zero;
    }

    public void RespawnPlayer()
    {
        if (activeCheckpoint == null)
        {
            Debug.LogWarning("No active checkpoint found!");
            return;
        }
        Debug.Log("YOU DIED");
        StartCoroutine(RespawnSequence());
    }

    private IEnumerator RespawnSequence()
    {
        // TODO: Disable player control via event?

        yield return new WaitForSeconds(respawnDelay);

        // Move player
        player.transform.position = activeCheckpoint.GetRespawnPosition();
        
        //TODO: Re-enable controls via event?
    }
}