using UnityEngine;
using Zenject;

public class HardRestartStates : BaseLoadLevelState, IState
{
    [Inject]
    public HardRestartStates(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer container, IEntityFactory entityFactory, IPersistantProgressService progressService, IStaticDataService staticDataService)
        : base(gameStateMachine, sceneLoader, container, entityFactory, progressService, staticDataService)
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

        _gameStateMachine.Enter<GameLoopState>();
        player.SetActive(true);
    }
}
