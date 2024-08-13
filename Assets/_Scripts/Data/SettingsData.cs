using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsData
{
    public LanguageEnum Language;
    public float SoundVolume;
    public float MusicVolume;
    public float Sensitivity;
    public GraphicsSettingsEnum GraphicsSettings;
    public bool LockFPS;
}
