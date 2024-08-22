using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuState : IState
{
    private const string LevelTestName = "TrainRoom";

    private readonly IGameStateMachine _gameStateMachine;
    private MainMenuUI _mainMenuUI;
    private readonly LoadingCurtain _curtain;
    private SettingsData _settingsData;

    [Inject]
    public MainMenuState(IGameStateMachine gameStateMachine, LoadingCurtain curtain, SettingsData settingsData)
    {
        _gameStateMachine = gameStateMachine;
        _curtain = curtain;
        _settingsData = settingsData;
    }

    public void Enter()
    {
        _curtain.Hide();
        _mainMenuUI = GameObject.FindObjectOfType<MainMenuUI>();
        _mainMenuUI.SetSettingsInStart();
        OnLoaded();
    }

    public void Exit()
    {
        _mainMenuUI.OnStartGameClicked -= StartGame;
        _mainMenuUI.OnSettingsClicked -= OpenSettings;
    }

    private void OnLoaded()
    {
        _mainMenuUI.OnStartGameClicked += StartGame;
        _mainMenuUI.OnSettingsClicked += OpenSettings;
    }

    private void StartGame()
    {
        _gameStateMachine.Enter<LoadProgressState>();
    }

    private void OpenSettings()
    {
        _mainMenuUI.SettingsCanvas.gameObject.SetActive(true);
    }
}
