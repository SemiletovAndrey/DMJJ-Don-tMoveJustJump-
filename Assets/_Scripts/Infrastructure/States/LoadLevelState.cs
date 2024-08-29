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
        InformProgressReaders();
        CameraFollow(player);
        _gameStateMachine.Enter<GameLoopState>();
        Debug.Log("OnLoad");

    }

    private void InitSaveTrigger(LevelStaticData levelStaticData)
    {
        int positionIndex = levelStaticData.CurrentCheckpointIndex;
        if (positionIndex == 0)
        {
            _entityFactory.CreateSaveTrigger(levelStaticData.Checkpoints[positionIndex]);

        }
        else if (positionIndex < levelStaticData.CurrentCheckpointIndex - 1)
        {
            positionIndex++;
            _entityFactory.CreateSaveTrigger(levelStaticData.Checkpoints[levelStaticData.CurrentCheckpointIndex]);
        }
    }

    private GameObject InitPlayer(LevelStaticData levelData)
    {
        GameObject player = _entityFactory.CreatePlayer(levelData.InitialPlayerPoint);

        return player;
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
            Debug.Log("Read");
        }
    }
    private void CameraFollow(GameObject hero)
    {
        Camera.main.GetComponent<CameraFollower>().Follow(hero);
    }
}
