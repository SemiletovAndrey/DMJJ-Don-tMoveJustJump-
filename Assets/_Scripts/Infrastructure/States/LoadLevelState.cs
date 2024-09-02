using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadLevelState : IPayloadState<string>
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private IEntityFactory _entityFactory;
    private IPersistantProgressService _progressService;
    private IStaticDataService _staticDataService;

    [Inject]
    public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IEntityFactory entityFactory, IPersistantProgressService progressService, IStaticDataService staticDataService)
    {
        this._gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _entityFactory = entityFactory;
        _progressService = progressService;
        _staticDataService = staticDataService;
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
        LevelStaticData levelData = _staticDataService.GetLevelStaticData();

        GameObject player = InitPlayer(levelData);
        InitHud();
        InitSaveTrigger(levelData);
        InitLevelTransfer(levelData);
        InformProgressReaders();
        CameraFollow(player);
        _gameStateMachine.Enter<GameLoopState>();
    }

    private void InitSaveTrigger(LevelStaticData levelStaticData)
    {
        int positionIndex = _progressService.Progress.WorldData.PositionOnLevel.CurrentCheckpointIndex;
        if (positionIndex < levelStaticData.Checkpoints.Count)
        {
            _entityFactory.CreateSaveTrigger(levelStaticData.Checkpoints[positionIndex]);
        }
    }

    private GameObject InitPlayer(LevelStaticData levelData)
    {
        GameObject player = _entityFactory.CreatePlayer(levelData.InitialPlayerPoint);

        return player;
    }
    
    private void InitLevelTransfer(LevelStaticData levelData)
    {
        _entityFactory.CreateLevelTransfer(levelData.LevelTransferStaticData.TransferTo,levelData.LevelTransferStaticData.Position);
    }

    private void InitHud()
    {
        _entityFactory.CreateHud();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader reader in _entityFactory.ProgressReader)
        {
            reader.LoadProgress(_progressService.Progress);
        }
    }
    private void CameraFollow(GameObject hero)
    {
        Camera.main.GetComponent<CameraFollower>().Follow(hero);
    }
}
