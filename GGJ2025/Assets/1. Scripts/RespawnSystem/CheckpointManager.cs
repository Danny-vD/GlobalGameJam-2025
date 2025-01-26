using UnityEngine;
using System.Collections;
using VDFramework.EventSystem;
using System;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }
    public Vector3 activeCheckpoint;
    public bool hasCheckpoint = false;

    [SerializeField]
    private float respawnDelay = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            EventManager.AddListener((GameplayEvents.CheckpointActivateEvent checkpointActivate) => SetActiveCheckpoint(checkpointActivate.checkpoint));
            // EventManager.AddListener((GameplayEvents.PlayerFailedEvent playerFailed) => RespawnPlayer());
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogWarning("No player found in scene!");
                return;
            }
            if (!Instance.hasCheckpoint)
            {
                Debug.Log("No active checkpoint found!");
                return;
            }

            Debug.Log($"Respawn at checkpoint {activeCheckpoint}");

            player.GetComponent<Rigidbody>().position = Instance.activeCheckpoint;
            Destroy(gameObject);
        }
    }

    public void SetActiveCheckpoint(GameObject checkpoint)
    {
        Debug.Log($"Checkpoint {checkpoint.transform.parent.name} activated!");
        hasCheckpoint = true;
        activeCheckpoint = checkpoint.transform.position;
    }
}