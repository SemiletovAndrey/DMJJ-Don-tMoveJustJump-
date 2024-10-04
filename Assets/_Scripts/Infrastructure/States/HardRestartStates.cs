using UnityEngine;
using Zenject;

public class HardRestartStates : BaseLoadLevelState, IState
{
    [Inject]
    public HardRestartStates(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer container, IEntityFactory entityFactory, IPersistantProgressService progressService, IStaticDataService staticDataService, LocalizationManager localizationManager)
        : base(gameStateMachine, sceneLoader, container, entityFactory, progressService, staticDataService, localizationManager)
    {
    }

    public void Enter()
    {
        _sceneLoader.LoadForRestart(SceneStaticService.CurrentLevel(), OnLoad);
    }

    public void Exit() { }

    private void OnLoad()
    {
        LevelStaticData levelData = _staticDataService.GetLevelStaticData();
        GameObject player = RestartPlayer(levelData);
        InitLevelTransfer(levelData);
        LoadDialoguesOnScene();
        _gameStateMachine.Enter<GameLoopState>();
        player.SetActive(true);
    }
}
