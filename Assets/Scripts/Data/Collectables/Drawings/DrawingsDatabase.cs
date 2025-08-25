using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;


[CreateAssetMenu(fileName = "DrawingsDatabase", menuName = "Datas/Collectables/DrawingsDatabase")]
public class DrawingsDatabase : ScriptableObject
{
    public List<DrawingData> drawingDatas = new List<DrawingData>();

    #if UNITY_EDITOR

    // reset database
    [Button("Reset Drawings")]
    public void RemoveAllDrawingsFromDatabase()
    {
        foreach (DrawingData drawing in drawingDatas)
        {
            drawing.isCollected = false;
        }
    }
    #endif
}
