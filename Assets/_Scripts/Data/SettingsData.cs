using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsData
{
    public event Action<float> OnSensitivityChanged;

    public LanguageEnum Language;
    public float SoundVolume;
    public float MusicVolume;
    public float _sensitivity;
    public GraphicsSettingsEnum GraphicsSettings;
    public bool LockFPS;

    public float Sensitivity
    {
        get => _sensitivity;
        set
        {
            if (_sensitivity != value)
            {
                _sensitivity = value;
                OnSensitivityChanged?.Invoke(_sensitivity);
            }
        }
    }
}
