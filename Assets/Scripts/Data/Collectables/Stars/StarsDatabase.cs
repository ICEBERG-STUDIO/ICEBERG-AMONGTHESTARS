using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "StarsDatabase", menuName = "Datas/Collectables/StarsDatabase")]
public class StarsDatabase : ScriptableObject
{
    public List<StarData> starsData = new List<StarData>();

    #if UNITY_EDITOR

    // reset database
    [Button("Reset Star")]
    public void RemoveAllDrawingsFromDatabase()
    {
        foreach (StarData star in starsData)
        {
            star.isCollected = false;
        }
    }
    #endif
}
