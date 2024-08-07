using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapState : IState
{
    private const string Initial = "Init";
    private const string LevelTestName = "SampleScene";
    
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
        Debug.Log("Enter Bootstrap State");
        _sceneLoader.Load(Initial, EnterLevelLoad);
    }

    private void EnterLevelLoad()
    {
        _gameStateMachine.Enter<LoadLevelState, string>(LevelTestName);
    }

    public void Exit()
    {

    }
}
