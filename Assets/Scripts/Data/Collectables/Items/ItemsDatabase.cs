using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "Datas/Collectables/ItemsDatabase")]
public class ItemsDatabase : ScriptableObject
{
    public List<ItemData> itemDatas = new List<ItemData>();


#if UNITY_EDITOR

    // reset database
    [Button]
    public void ResetData()
    {
        foreach (ItemData item in itemDatas)
        {
            item.isEquiped = false;
        }
    }
    #endif
}
