using System.Drawing;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SkillData
{
    public string name;

    public GenericData genericData;

    public bool isUnlocked;

    public string controls;

    public SkillData()
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

        isUnlocked = false;
    }

}
