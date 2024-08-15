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


    [Inject]
    public MainMenuState(IGameStateMachine gameStateMachine, LoadingCurtain curtain)
    {
        _gameStateMachine = gameStateMachine;
        _curtain = curtain;
    }

    public void Enter()
    {
        _curtain.Hide();
        _mainMenuUI = GameObject.FindObjectOfType<MainMenuUI>();
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
        _gameStateMachine.Enter<LoadLevelState, string>(LevelTestName);
    }

    private void OpenSettings()
    {
        _mainMenuUI.SettingsCanvas.gameObject.SetActive(true);
    }
}
