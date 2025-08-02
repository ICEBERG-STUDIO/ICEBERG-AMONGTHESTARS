
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
            if (drawing.collectablesData.inInventory == false) return false;
        }

        return true;
    }

    // reset inventory
    public void RemoveAllDrawingsFromInventory()
    {
        foreach (DrawingsData drawing in _drawingsDatabase.drawingsData)
        {
            drawing.collectablesData.inInventory = false;
        }
    }
}
