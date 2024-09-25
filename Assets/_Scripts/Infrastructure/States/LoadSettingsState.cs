using UnityEngine;
using Zenject;

public class LoadSettingsState : IState
{
    private const string MainMenuName = "MainMenuScene";

    private readonly IGameStateMachine _gameStateMachine;
    private readonly ISaveSettingsService _saveSettingsService;
    private SettingsData _settingsData;
    private readonly SceneLoader _sceneLoader;


    [Inject]
    public LoadSettingsState(IGameStateMachine gameStateMachine, ISaveSettingsService saveSettingsService, SettingsData settingsData, SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _saveSettingsService = saveSettingsService;
        _settingsData = settingsData;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        Debug.Log("LoadSettingState");
        LoadProgressOrInitNew();
        _sceneLoader.Load(MainMenuName, OnLoad);
    }

    private void OnLoad()
    {
        _gameStateMachine.Enter<MainMenuState>();
    }

    public void Exit()
    {
        
    }

    private void LoadProgressOrInitNew()
    {
        SettingsData loadedSettings = _saveSettingsService.LoadSettings();
        if (loadedSettings != null)
        {
            _settingsData.Language = loadedSettings.Language;
            _settingsData.MusicVolume = loadedSettings.MusicVolume;
            _settingsData.SoundVolume = loadedSettings.SoundVolume;
            _settingsData.Sensitivity = loadedSettings.Sensitivity;
            _settingsData.GraphicsSettings = loadedSettings.GraphicsSettings;
        }
        else
        {
            NewSettings();
        }
    }

    private SettingsData NewSettings()
    {
        Debug.Log("New Settings");
        _settingsData.Language = "en_US";
        _settingsData.MusicVolume = 0.5f;
        _settingsData.SoundVolume = 0.5f;
        _settingsData.Sensitivity = 100f;
        _settingsData.GraphicsSettings = GraphicsSettingsEnum.Low;
        _saveSettingsService.SaveSettings(_settingsData);
        return _settingsData;

    }
}
