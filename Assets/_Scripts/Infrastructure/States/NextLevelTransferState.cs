using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NextLevelTransferState : IPayloadState<string>
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private DiContainer _container;
    private IEntityFactory _entityFactory;
    private IPersistantProgressService _progressService;
    private IStaticDataService _staticDataService;
    private string nextSceneName;

    [Inject]
    public NextLevelTransferState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer container, IEntityFactory entityFactory, IPersistantProgressService progressService, IStaticDataService staticDataService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _container = container;
        _entityFactory = entityFactory;
        _progressService = progressService;
        _staticDataService = staticDataService;
    }

    public void Enter(string payload)
    {
        nextSceneName = payload;
        _sceneLoader.Load(payload, OnLoad);
    }
    public void Exit()
    {
        
    }

    private void OnLoad()
    {
        LevelStaticData levelData = _staticDataService.GetLevelStaticData(nextSceneName);

        GameObject player = RestartPlayer(levelData);
        InitSaveTrigger(levelData);
        InitLevelTransfer(levelData);
        _gameStateMachine.Enter<GameLoopState>();

        player.SetActive(true);
    }

    private GameObject RestartPlayer(LevelStaticData levelStaticData)
    {
        GameObject player = _container.ResolveId<GameObject>("Player");

        HeroMove heroMove = player.GetComponent<HeroMove>();
        heroMove.RestartRotation();
        player.GetComponent<HeroDeath>().ResetColor();
        heroMove.Warp(levelStaticData.InitialPlayerPoint);

        CameraFollow(player);
        InformProgressReaders();
        return player;
    }

    private void CameraFollow(GameObject hero)
    {
        Camera.main.GetComponent<CameraFollower>().Follow(hero);
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader reader in _entityFactory.ProgressReader)
        {
            reader.LoadProgress(_progressService.Progress);
            Debug.Log("Read");
        }
    }

    private void InitSaveTrigger(LevelStaticData levelStaticData)
    {
        int positionIndex = _progressService.Progress.WorldData.PositionOnLevel.CurrentCheckpointIndex;
        if (positionIndex < levelStaticData.Checkpoints.Count)
        {
            _entityFactory.CreateSaveTrigger(levelStaticData.Checkpoints[positionIndex]);
        }
    }

    private void InitLevelTransfer(LevelStaticData levelData)
    {
        _entityFactory.CreateLevelTransfer(levelData.LevelTransferStaticData.TransferTo, levelData.LevelTransferStaticData.Position);
    }

}
