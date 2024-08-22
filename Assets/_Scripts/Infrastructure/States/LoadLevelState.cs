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
    private IPersistantProgressService _progressService;

    [Inject]
    public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IEntityFactory entityFactory, IPersistantProgressService progressService)
    {
        this._gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _entityFactory = entityFactory;
        _progressService = progressService;
    }


    public void Enter(string sceneName)
    {
        _curtain.Show();
        _entityFactory.CleanUp();
        _sceneLoader.Load(sceneName, OnLoad);
    }

    public void Exit()
    {
        _curtain.Hide();
    }

    private void OnLoad()
    {
        GameObject player = InitPlayer();
        InitHud();
        InformProgressReaders();
        CameraFollow(player);
        _gameStateMachine.Enter<GameLoopState>();
        Debug.Log("OnLoad");

    }

    private GameObject InitPlayer()
    {
        Vector3 positionPlayer = GameObject.FindWithTag(InitialPointTag).transform.position;
        GameObject player = _entityFactory.CreatePlayer(positionPlayer);
        player.transform.position = positionPlayer;
        return player;
    }

    private void InitHud()
    {
        GameObject hud = _entityFactory.CreateHud();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader reader in _entityFactory.ProgressReader)
        {
            reader.LoadProgress(_progressService.Progress);
            Debug.Log("Read");
        }
    }
    private void CameraFollow(GameObject hero)
    {
        Camera.main.GetComponent<CameraFollower>().Follow(hero);
    }
}
