using System.Collections.Generic;

[System.Serializable]
public class LocalizationTextEntry
{
    public string key;
    public string Value;
}

[System.Serializable]
public class LocalizationData
{
    public List<LocalizationTextEntry> Text;
}