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
    private IEntityFactory _entityFactory;

    [Inject]
    public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IEntityFactory entityFactory)
    {
        this._gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _entityFactory = entityFactory;
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
        Vector3 positionPlayer = GameObject.FindWithTag(InitialPointTag).transform.position;
        GameObject player = _entityFactory.CreatePlayer(positionPlayer);
        GameObject hud = _entityFactory.CreateHud();
        CameraFollow(player);
        _gameStateMachine.Enter<GameLoopState>();
        Debug.Log("OnLoad");

    }

    
    private void CameraFollow(GameObject hero)
    {
        Camera.main.GetComponent<CameraFollower>().Follow(hero);
    }
}
