using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadLevelState : BaseLoadLevelState, IPayloadState<string>
{
    private readonly LoadingCurtain _curtain;

    [Inject]
    public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer container, IEntityFactory entityFactory, IPersistantProgressService progressService, IStaticDataService staticDataService, LoadingCurtain curtain)
        : base(gameStateMachine, sceneLoader, container, entityFactory, progressService, staticDataService)
    {
        _curtain = curtain;
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
        CameraFollow(player);
        InformProgressReaders();

        _gameStateMachine.Enter<GameLoopState>();
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
}
