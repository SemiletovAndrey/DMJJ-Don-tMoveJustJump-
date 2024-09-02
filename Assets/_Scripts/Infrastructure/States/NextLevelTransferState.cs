using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NextLevelTransferState : BaseLoadLevelState, IPayloadState<string>
{
    private string nextSceneName;

    [Inject]
    public NextLevelTransferState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer container, IEntityFactory entityFactory, IPersistantProgressService progressService, IStaticDataService staticDataService)
        : base(gameStateMachine, sceneLoader, container, entityFactory, progressService, staticDataService)
    {
    }

    public void Enter(string payload)
    {
        nextSceneName = payload;
        _sceneLoader.Load(payload, OnLoad);
    }

    public void Exit() { }

    private void OnLoad()
    {
        LevelStaticData levelData = _staticDataService.GetLevelStaticData(nextSceneName);

        GameObject player = RestartPlayer(levelData);
        InitSaveTrigger(levelData);
        InitLevelTransfer(levelData);

        _gameStateMachine.Enter<GameLoopState>();
        player.SetActive(true);
    }

}
