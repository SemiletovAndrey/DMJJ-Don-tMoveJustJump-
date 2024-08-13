using System;
using UnityEngine;
using Zenject;

public class BootstrapState : IState
{
    private const string Initial = "Init";
    private const string MainMenuName = "MainMenuScene";

    private readonly IGameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    [Inject]
    public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        this._gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        _sceneLoader.Load(Initial, EnterLevelLoad);
    }

    private void EnterLevelLoad()
    {
        _sceneLoader.Load(MainMenuName, OnLoad);

    }

    private void OnLoad()
    {
        _gameStateMachine.Enter<LoadSettingsState>();
    }

    public void Exit()
    {

    }
}
