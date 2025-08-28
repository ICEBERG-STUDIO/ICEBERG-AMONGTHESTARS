using UnityEngine;

[System.Serializable]
public class GenericData 
{
    [Header("Infos")]
    public string label;
    public string caption;
    public string ID;

    [Header("Graphics")]
    public Sprite sprite;
    public Sprite icon;
}
