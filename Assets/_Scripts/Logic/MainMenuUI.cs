using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuUI : MonoBehaviour
{
    public Canvas SettingsCanvas;
    public Action OnStartGameClicked;
    public Action OnSettingsClicked;

    private ISaveSettingsService _saveSettingsService;
    private SettingsData _settingsData;

    [Inject(Id = "MusicSlider")] private Slider _musicSlider;
    [Inject(Id = "SoundSlider")] private Slider _soundSlider;
    [Inject(Id = "SensitivitySlider")] private Slider _sensitivitySlider;
    [Inject(Id = "GraphicQuality")] private TMP_Dropdown _graphicQualityDropdown;
    [Inject(Id = "LockFPS")] private Toggle _lockFPSToggle;
    [Inject(Id = "Language")] private TMP_Dropdown _languageDropdown;

    [Inject]
    public void Construct(ISaveSettingsService saveSettingsService, SettingsData settingsData)
    {
        _saveSettingsService = saveSettingsService;
        _settingsData = settingsData;
    }


    private void Start()
    {
        SettingsCanvas.gameObject.SetActive(false);
        SetSettingsInStart();
    }

    public void StartGameButton()
    {
        OnStartGameClicked?.Invoke();
    }

    public void SettingsButton()
    {
        OnSettingsClicked?.Invoke();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ApplyAndContinue()
    {
        ApplySettings();
        SettingsCanvas.gameObject.SetActive(false);
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
        settingsData.LockFPS = _lockFPSToggle.isOn;
        settingsData.Language = DropdownEnumUtility.GetSelectedEnumValue<LanguageEnum>(_languageDropdown);
        settingsData.GraphicsSettings = DropdownEnumUtility.GetSelectedEnumValue<GraphicsSettingsEnum>(_graphicQualityDropdown);

        return settingsData;
    }

    public void SetSettingsInStart()
    {
        _musicSlider.value = _settingsData.MusicVolume;
        _soundSlider.value = _settingsData.SoundVolume;
        _sensitivitySlider.value = _settingsData.Sensitivity;
        _lockFPSToggle.isOn = _settingsData.LockFPS;
        DropdownEnumUtility.SetupDropdown(_languageDropdown, _settingsData.Language);
        DropdownEnumUtility.SetupDropdown(_graphicQualityDropdown, _settingsData.GraphicsSettings);
    }
}
