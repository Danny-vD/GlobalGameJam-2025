using UnityEngine;
using System.Collections;
using VDFramework.EventSystem;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }
    private Vector3 activeCheckpoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnDelay = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            EventManager.AddListener((GameplayEvents.CheckpointActivateEvent checkpointActivate) => SetActiveCheckpoint(checkpointActivate.checkpoint));
            EventManager.AddListener((GameplayEvents.PlayerFailedEvent playerFailed) => RespawnPlayer());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetActiveCheckpoint(GameObject checkpoint)
    {
        Debug.Log($"Checkpoint {checkpoint.transform.parent.name} activated!");
        activeCheckpoint = checkpoint.transform.position;
    }

    public void RespawnPlayer()
    {
        Debug.Log("YOU DIED");
        if (activeCheckpoint == null)
        {
            Debug.LogWarning("No active checkpoint found!");
            return;
        }
        Debug.Log($"Respawn at checkpoint {activeCheckpoint}");
    }
}