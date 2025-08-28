
using UnityEngine;

public class ItemsDatabaseManager : MonoBehaviour
{
    public ItemsDatabase itemsDatabase;

    public ItemData GetItem(string id) => itemsDatabase.itemDatas.Find(x => x.genericData.ID == id);
}
