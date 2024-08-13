using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenuUI : MonoBehaviour
{
    public Canvas PauseCanvas;

    private ISaveSettingsService _saveSettingsService;
    private SettingsData _settingsData;

    [Inject(Id = "MusicSlider")] private Slider _musicSlider;
    [Inject(Id = "SoundSlider")] private Slider _soundSlider;
    [Inject(Id = "SensitivitySlider")] private Slider _sensitivitySlider;
    

    [Inject]
    public void Construct(ISaveSettingsService saveSettingsService)
    {
        _saveSettingsService = saveSettingsService;
    }


    private void Start()
    {
        PauseCanvas.gameObject.SetActive(false);
        _settingsData = _saveSettingsService.LoadSettings();
        SetSettingsInStart();
    }

   
    public void ExitButton()
    {
        Application.Quit();
    }

    public void ApplyAndContinue()
    {
        ApplySettings();
        PauseCanvas.gameObject.SetActive(false);
    }

    private void ApplySettings()
    {
        SettingsData settingsData = GetNewSettingData();
        _saveSettingsService.SaveSettings(settingsData);
    }

    private SettingsData GetNewSettingData()
    {
        SettingsData settingsData = new SettingsData();
        settingsData.MusicVolume = _musicSlider.value;
        settingsData.SoundVolume = _soundSlider.value;
        settingsData.Sensitivity = _sensitivitySlider.value;
        
        return settingsData;
    }

    public void SetSettingsInStart()
    {
        _musicSlider.value = _settingsData.MusicVolume;
        _soundSlider.value = _settingsData.SoundVolume;
        _sensitivitySlider.value = _settingsData.Sensitivity;
    }
}
