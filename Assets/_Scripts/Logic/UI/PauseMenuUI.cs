using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Canvas PauseCanvas;

    private ISaveSettingsService _saveSettingsService;
    private IGameStateMachine _gameStateMachine;
    private IInputService _inputService;
    private SettingsData _settingsData;
    private bool _isActivePause = false;
    private bool _isAnimation = false;
    private UIWindowAnimator _windowAnimator;

    [Inject(Id = "MusicSlider")] private Slider _musicSlider;
    [Inject(Id = "SoundSlider")] private Slider _soundSlider;
    [Inject(Id = "SensitivitySlider")] private Slider _sensitivitySlider;
    [Inject(Id = "PauseContainer")]private RectTransform _pauseRectContainer;


    [Inject]
    public void Construct(ISaveSettingsService saveSettingsService, IInputService inputService, SettingsData settingsData, IGameStateMachine gameStateMachine)
    {
        _saveSettingsService = saveSettingsService;
        _inputService = inputService;
        _settingsData = settingsData;
        _gameStateMachine = gameStateMachine;
    }


    private void Start()
    {
        _windowAnimator = new UIWindowAnimator(_pauseRectContainer);
        PauseCanvas.gameObject.SetActive(false);
        SetSettingsInStart();
    }

    private void Update()
    {
        if (_inputService.IsMenuPause())
        {
            if (!_isActivePause)
            {
                ActivatePause();
            }
            else
            {
                DeactivatePause();
            }

        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ApplyAndContinue()
    {
        ApplySettings();
        DeactivatePause();
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

    private void ActivatePause()
    {
        if (_isAnimation)
        {
            return;
        }
        _isAnimation = true;
        PauseCanvas.gameObject.SetActive(true);
        _windowAnimator.AnimateOnWindow(()=>
        OnContainer()
        ); 
    }

    private void DeactivatePause()
    {
        if (_isAnimation)
        {
            return;
        }
        _gameStateMachine.Enter<GameLoopState>();
        _isAnimation = true;
        _windowAnimator.AnimateOffWindow(()=>
        OffContainer()
        );
    }

    private void OffContainer()
    {
        PauseCanvas.gameObject.SetActive(false);
        _isActivePause = false;
        _isAnimation = false;
    }

    private void OnContainer()
    {
        PauseCanvas.gameObject.SetActive(true);
        _isActivePause = true;
        _isAnimation = false;
        _gameStateMachine.Enter<GamePauseState>();
    }
}
