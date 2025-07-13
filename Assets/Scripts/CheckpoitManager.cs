using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private List<CheckpointsData> checkpointList;
    public static List<CheckpointsData> checkpoints;
    [SerializeField] public GameObject _player;
    public static GameObject player;

    private void Awake()
    {
        checkpoints = checkpointList;
        player = _player;
    }

    public static void TeleportTo(string checkpointName)
    {
        var cp = checkpoints.FirstOrDefault(c => c.checkpointName == checkpointName && c.IsCheckpointFound);
        if (cp != null)
        {
            // Déplace le joueur
            player.GetComponent<Rigidbody2D>().transform.position = cp.checkpointPosition;
        }
    }
}