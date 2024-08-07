using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadLevelState : IPayloadState<string>
{
    private const string InitialPointTag = "InitialPoint";

    private readonly IGameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;

    [Inject]
    public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
    {
        this._gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
    }


    public void Enter(string sceneName)
    {
        _curtain.Show();
        _sceneLoader.Load(sceneName, OnLoad);
    }

    public void Exit()
    {
        _curtain.Hide();
    }

    private void OnLoad()
    {
        Debug.Log("OnLoad");
        //GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
        //GameObject hero = Instantiate(AssetAddress.HeroPath, initialPoint.transform.position);
        //Instantiate(AssetAddress.HudPath);
        _gameStateMachine.Enter<GameLoopState>();
    }

    private void Instantiate(string hudPath)
    {
        throw new NotImplementedException();
    }

    private GameObject Instantiate(string heroPath, Vector3 position)
    {
        throw new NotImplementedException();
    }
}
