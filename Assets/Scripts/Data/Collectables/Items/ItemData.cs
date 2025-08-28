using System.Drawing;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemData
{
    public string name;

    public GenericData genericData;

    public bool isEquiped;

    public Component Component;

    public ItemData()
    {
        name = "default item";

        //generic data
        genericData = new GenericData();

        genericData.label = "default label";
        genericData.caption = "default caption";
        genericData.ID = "default id";

        genericData.sprite = default;
        genericData.icon = default;
        //color = Color.white;

        isEquiped = false;
    }

    public UnityEvent OnUse;

    // EDIT
    public void Equip() => isEquiped = true;

    public virtual void Use(GameObject user)
    {
        Debug.Log($"Equip {genericData.label}");
    }

}
