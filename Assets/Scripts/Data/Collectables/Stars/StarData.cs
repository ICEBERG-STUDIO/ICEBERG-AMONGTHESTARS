using UnityEngine;

[System.Serializable]
public class StarData
{
    public string name;

    public GenericData collectablesData;

    [HideInInspector]
    public bool isCollected;

    public BIOMES biome;
}
