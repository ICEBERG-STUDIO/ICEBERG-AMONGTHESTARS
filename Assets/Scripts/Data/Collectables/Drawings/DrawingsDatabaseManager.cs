
using UnityEngine;

public class DrawingsDatabaseManager : MonoBehaviour
{
    public DrawingsDatabase drawingsDatabase;

    public DrawingData GetDrawing(int id) => drawingsDatabase.drawingDatas[id];

    // -- UNLOCK END --
    public bool GoodEndingUnlock()
    {
        foreach (DrawingData drawing in drawingsDatabase.drawingDatas) 
        {
            if (drawing.isCollected == false) return false;
        }

        return true;
    }
}
