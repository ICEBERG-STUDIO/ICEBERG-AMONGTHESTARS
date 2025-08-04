
using UnityEngine;
using System.Collections.Generic;

public class DrawingsDatabaseManager : MonoBehaviour
{
    [SerializeField] private DrawingsDatabase _drawingsDatabase;

    public DrawingsData GetDrawing(int id) => _drawingsDatabase.drawingsData[id];


    public bool GoodEndingUnlock()
    {
        foreach (DrawingsData drawing in _drawingsDatabase.drawingsData) 
        {
            Debug.Log("là");
            if (drawing.collectablesInfos.inInventory == false) return false;
        }

        return true;
    }
}
