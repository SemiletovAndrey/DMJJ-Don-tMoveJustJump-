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
    [Inject(Id = "LockFPS")] private Slider _lockFPSToggle;
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
        GetSettingData();
        Application.targetFrameRate = (int)_lockFPSToggle.value;
        _saveSettingsService.SaveSettings(_settingsData);
    }

    private void GetSettingData()
    {
        _settingsData.MusicVolume = _musicSlider.value;
        _settingsData.SoundVolume = _soundSlider.value;
        _settingsData.Sensitivity = _sensitivitySlider.value;
        _settingsData.LockFPS = _lockFPSToggle.value;
        _settingsData.GraphicsSettings = DropdownEnumUtility.GetSelectedEnumValue<GraphicsSettingsEnum>(_graphicQualityDropdown);
    }

    public void SetSettingsInStart()
    {
        _musicSlider.value = _settingsData.MusicVolume;
        _soundSlider.value = _settingsData.SoundVolume;
        _sensitivitySlider.value = _settingsData.Sensitivity;
        _lockFPSToggle.value = _settingsData.LockFPS;

        int index = SetDropDownLanguage(_settingsData.Language);
        _languageDropdown.value = index;
        DropdownEnumUtility.SetupDropdown(_graphicQualityDropdown, _settingsData.GraphicsSettings);
    }

    public int SetDropDownLanguage(string language)
    {
        string cipherLanguage;
        switch (language)
        {
            case "en_US":
                cipherLanguage = "English";
                break;
            case "ru_RU":
                cipherLanguage = "Русский";
                break;
            case "ua_UA":
                cipherLanguage = "Українська";
                break;
            default:
                cipherLanguage = "English";
                break;
        }
        return _languageDropdown.options.FindIndex(option => option.text == cipherLanguage);
    }
}
