using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StarsDatabase", menuName = "Datas/Collectables/StarsDatabase")]
public class StarsDatabase : ScriptableObject
{
    public List<StarsData> starsData = new List<StarsData>();
}
