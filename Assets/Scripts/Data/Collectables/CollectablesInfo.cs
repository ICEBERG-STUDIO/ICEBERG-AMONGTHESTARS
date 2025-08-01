using UnityEngine;

[System.Serializable]
public class CollectablesInfo
{
    public string caption;

    public BIOME biome;

    //[HideInInspector]
    public bool inInventory;

    [Header("Graphics")]
    public Sprite sprite;
}
