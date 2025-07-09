using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerSettings
{
    [Serializable]
    public struct DataSettingsElement
    {
        public string locale;

        public DataSettingsElement(string locale)
        {
            this.locale = locale;
        }
    }

    public DataSettingsElement data;

    public PlayerSettings()
    {
        data = new DataSettingsElement();
    }
}