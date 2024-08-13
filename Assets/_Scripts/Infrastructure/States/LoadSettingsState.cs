using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadSettingsState : IState
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ISaveSettingsService _saveSettingsService;
    private SettingsData _settingsData = new SettingsData();

    [Inject]
    public LoadSettingsState(IGameStateMachine gameStateMachine, ISaveSettingsService saveSettingsService)
    {
        _gameStateMachine = gameStateMachine;
        _saveSettingsService = saveSettingsService;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();
        _gameStateMachine.Enter<MainMenuState>();

    }

    public void Exit()
    {
        
    }

    private void LoadProgressOrInitNew()
    {
        _settingsData = _saveSettingsService.LoadSettings() ?? NewProgress();
    }

    private SettingsData NewProgress()
    {
        SettingsData settings = new SettingsData();
        Debug.Log("New Progress");
        settings.Language = LanguageEnum.English;
        settings.MusicVolume = 0.5f;
        settings.SoundVolume = 0.5f;
        settings.Sensitivity = 100f;
        settings.GraphicsSettings = GraphicsSettingsEnum.Low;
        _saveSettingsService.SaveSettings(settings);
        return settings;

    }
}
