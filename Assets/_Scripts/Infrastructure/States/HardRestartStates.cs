using UnityEngine;
using Zenject;

public class HardRestartStates : IState
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private DiContainer _container;
    private IEntityFactory _entityFactory;
    private IPersistantProgressService _progressService;

    [Inject]
    public HardRestartStates(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer container, IEntityFactory entityFactory, IPersistantProgressService progressService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _container = container;
        _entityFactory = entityFactory;
        _progressService = progressService;
    }
    public void Enter()
    {
        _sceneLoader.LoadForRestart(HeroMove.CurrentLevel(), OnLoad);
    }

    public void Exit()
    {

    }
    private void OnLoad()
    {
        GameObject player = _container.ResolveId<GameObject>("Player");

        HeroMove heroMove = player.GetComponent<HeroMove>();
        heroMove.LoadProgress(_progressService.Progress);
        heroMove.RestartRotation();
        player.GetComponent<HeroDeath>().ResetColor();

        CameraFollow(player);
        InformProgressReaders();

        _gameStateMachine.Enter<GameLoopState>();

        player.SetActive(true);
        Debug.Log("OnLoad");
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
