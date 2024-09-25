using System;

[Serializable]
public class SettingsData
{
    public event Action<float> OnSensitivityChanged;

    public string Language;
    public float SoundVolume;
    public float MusicVolume;
    public float _sensitivity;
    public GraphicsSettingsEnum GraphicsSettings;
    public float LockFPS;

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
