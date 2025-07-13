using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CheckpointsData", menuName = "Checkpoints/Checkpoint Data")]
public class CheckpointsData : ScriptableObject
{
    public string checkpointName;
    public Vector3 checkpointPosition;
    public bool IsCheckpointFound = false;
    
}
