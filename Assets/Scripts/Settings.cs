using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _local;
    PlayerSettings savedSettings;


    private void Awake()
    {
        LoadSettings();
    }

    public void SaveSettings()
    {
        PlayerSettings playerSettings;

        if (savedSettings != null)
            playerSettings = savedSettings;
        else
            playerSettings = new PlayerSettings();

        int selectedLocal = _local.value;

        switch (selectedLocal)
        {
            case 0:
                playerSettings.data.locale = "EN";
                break;
            case 1:
                playerSettings.data.locale = "FR";
                break;
            default:
                break;
        }
        _local.value = selectedLocal;
        LocalizationManager.locale = playerSettings.data.locale;
        SaveSystem.SavePlayerSettings(playerSettings);
    }

    private void LoadSettings()
    {
        PlayerSettings settings = SaveSystem.LoadPlayerSettings();
        if (settings != null)
        {
            savedSettings = settings;
            LocalizationManager.locale = savedSettings.data.locale ?? "EN";
            switch (savedSettings.data.locale)
            {
                case "EN":
                    _local.value = 0;
                    break;
                case "FR":
                    _local.value = 1;
                    break;
                default:
                    _local.value = 0;
                    break;
            }
        }
    }
}