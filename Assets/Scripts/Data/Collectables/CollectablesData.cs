using UnityEngine;

[System.Serializable]
public class CollectablesData 
{
    public string caption;

    public BIOME biome;

    //[HideInInspector]
    public bool inInventory;

    [Header("Graphics")]
    public Sprite sprite;
}
