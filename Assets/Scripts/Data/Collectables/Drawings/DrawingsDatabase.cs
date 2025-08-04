using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "DrawingsDatabase", menuName = "Datas/Collectables/DrawingsDatabase")]
public class DrawingsDatabase : ScriptableObject
{
    public List<DrawingsData> drawingsData = new List<DrawingsData>();
}
