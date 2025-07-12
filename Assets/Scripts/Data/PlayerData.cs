using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    
    [Serializable]
    public struct LastPlayerPosition
    {
        public float LastCheckpointPositionX;
        public float LastCheckpointPositionY;
        public float LastCheckpointPositionZ;

        public LastPlayerPosition(float lastCheckpointPositionX, float lastCheckpointPositionY,
            float lastCheckpointPositionZ)
        {
            LastCheckpointPositionX = lastCheckpointPositionX;
            LastCheckpointPositionY = lastCheckpointPositionY;
            LastCheckpointPositionZ = lastCheckpointPositionZ;
        }
    }
    
    [Serializable]
    public struct DataPlayerElement
    {
        public string LastPlanetName;
        public LastPlayerPosition LastPlayerPosition;
        
        public DataPlayerElement(string LastPlanetName, LastPlayerPosition LastPlayerPosition)
        {
            this.LastPlanetName = LastPlanetName;
            this.LastPlayerPosition = LastPlayerPosition;
        }
    }

    public DataPlayerElement data;

    public PlayerData()
    {
        data = new DataPlayerElement();
    }
}