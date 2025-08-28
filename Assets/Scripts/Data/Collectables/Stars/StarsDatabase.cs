using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "StarsDatabase", menuName = "Datas/Collectables/StarsDatabase")]
public class StarsDatabase : ScriptableObject
{
    public List<StarData> starDatas = new List<StarData>();

    #if UNITY_EDITOR

    // reset database
    [Button("Reset Star")]
    public void RemoveAllDrawingsFromDatabase()
    {
        foreach (StarData star in starDatas)
        {
            star.isCollected = false;
        }
    }
    #endif
}
