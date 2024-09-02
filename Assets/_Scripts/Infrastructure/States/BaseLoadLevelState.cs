using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class BaseLoadLevelState
{
    protected readonly IGameStateMachine _gameStateMachine;
    protected readonly SceneLoader _sceneLoader;
    protected DiContainer _container;
    protected IEntityFactory _entityFactory;
    protected IPersistantProgressService _progressService;
    protected IStaticDataService _staticDataService;

    protected BaseLoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer container, IEntityFactory entityFactory, IPersistantProgressService progressService, IStaticDataService staticDataService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _container = container;
        _entityFactory = entityFactory;
        _progressService = progressService;
        _staticDataService = staticDataService;
    }

    protected void CameraFollow(GameObject hero)
    {
        Camera.main.GetComponent<CameraFollower>().Follow(hero);
    }

    protected void InformProgressReaders()
    {
        foreach (ISavedProgressReader reader in _entityFactory.ProgressReader)
        {
            reader.LoadProgress(_progressService.Progress);
            Debug.Log("Read");
        }
    }

    protected GameObject RestartPlayer(LevelStaticData levelData)
    {
        GameObject player = _container.ResolveId<GameObject>("Player");

        HeroMove heroMove = player.GetComponent<HeroMove>();
        heroMove.RestartRotation();
        player.GetComponent<HeroDeath>().ResetColor();
        if (levelData != null)
        {
            heroMove.Warp(levelData.InitialPlayerPoint);
        }

        CameraFollow(player);
        InformProgressReaders();
        return player;
    }

    protected void InitLevelTransfer(LevelStaticData levelData)
    {
        _entityFactory.CreateLevelTransfer(levelData.LevelTransferStaticData.TransferTo, levelData.LevelTransferStaticData.Position);
    }

    protected void InitSaveTrigger(LevelStaticData levelData)
    {
        int positionIndex = _progressService.Progress.WorldData.PositionOnLevel.CurrentCheckpointIndex;
        if (positionIndex < levelData.Checkpoints.Count)
        {
            _entityFactory.CreateSaveTrigger(levelData.Checkpoints[positionIndex]);
        }
    }
}
