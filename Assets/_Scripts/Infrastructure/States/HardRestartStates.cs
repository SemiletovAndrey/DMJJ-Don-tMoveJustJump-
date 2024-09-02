using UnityEngine;
using Zenject;

public class HardRestartStates : IState
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private DiContainer _container;
    private IEntityFactory _entityFactory;
    private IPersistantProgressService _progressService;
    private IStaticDataService _staticDataService;

    [Inject]
    public HardRestartStates(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer container, IEntityFactory entityFactory, IPersistantProgressService progressService, IStaticDataService staticDataService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _container = container;
        _entityFactory = entityFactory;
        _progressService = progressService;
        _staticDataService = staticDataService;
    }
    public void Enter()
    {
        _sceneLoader.LoadForRestart(SceneStaticService.CurrentLevel(), OnLoad);
    }

    public void Exit()
    {

    }
    private void OnLoad()
    {
        LevelStaticData levelData = _staticDataService.GetLevelStaticData();
        GameObject player = RestartPlayer();
        InitLevelTransfer(levelData);

        _gameStateMachine.Enter<GameLoopState>();

        player.SetActive(true);
    }

    private GameObject RestartPlayer()
    {
        GameObject player = _container.ResolveId<GameObject>("Player");

        HeroMove heroMove = player.GetComponent<HeroMove>();
        heroMove.LoadProgress(_progressService.Progress);
        heroMove.RestartRotation();
        player.GetComponent<HeroDeath>().ResetColor();

        CameraFollow(player);
        InformProgressReaders();
        return player;
    }

    private void InitLevelTransfer(LevelStaticData levelData)
    {
        _entityFactory.CreateLevelTransfer(levelData.LevelTransferStaticData.TransferTo, levelData.LevelTransferStaticData.Position);
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
}
