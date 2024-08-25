using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Canvas PauseCanvas;

    private ISaveSettingsService _saveSettingsService;
    private IInputService _inputService;
    private SettingsData _settingsData;

    [Inject(Id = "MusicSlider")] private Slider _musicSlider;
    [Inject(Id = "SoundSlider")] private Slider _soundSlider;
    [Inject(Id = "SensitivitySlider")] private Slider _sensitivitySlider;
    

    [Inject]
    public void Construct(ISaveSettingsService saveSettingsService, IInputService inputService, SettingsData settingsData)
    {
        _saveSettingsService = saveSettingsService;
        _inputService = inputService;
        _settingsData = settingsData;
    }


    private void Start()
    {
        PauseCanvas.gameObject.SetActive(false);
        SetSettingsInStart();
    }

    private void Update()
    {
        if (_inputService.IsMenuPause())
        {
            ActivePause();
        }
    }

    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void ApplyAndContinue()
    {
        ApplySettings();
        PauseCanvas.gameObject.SetActive(false);
    }

    public void SetSettingsInStart()
    {
        _musicSlider.value = _settingsData.MusicVolume;
        _soundSlider.value = _settingsData.SoundVolume;
        _sensitivitySlider.value = _settingsData.Sensitivity;
    }

    public void OnSensitivityValue(float sensitivity)
    {
        _settingsData.Sensitivity = sensitivity;
    }

    private void ApplySettings()
    {
        GetNewSettingData();
        _saveSettingsService.SaveSettings(_settingsData);
    }

    private void GetNewSettingData()
    {
        _settingsData.MusicVolume = _musicSlider.value;
        _settingsData.SoundVolume = _soundSlider.value;
        _settingsData.Sensitivity = _sensitivitySlider.value;
    }

    private void ActivePause()
    {
        PauseCanvas.gameObject.SetActive(true);
    }
}
