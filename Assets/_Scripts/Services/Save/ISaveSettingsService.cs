using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveSettingsService
{
    public SettingsData LoadSettings();
    public void SaveSettings(SettingsData settings);
}
