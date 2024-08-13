using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSettingService : ISaveSettingsService
{
    private const string SettingsKey = "GameSettings";

    public SettingsData LoadSettings() =>
        PlayerPrefs.GetString(SettingsKey)?.ToDeserialized<SettingsData>();

    public void SaveSettings(SettingsData settings)
    {
        PlayerPrefs.SetString(SettingsKey, settings.ToJson());
    }
}
