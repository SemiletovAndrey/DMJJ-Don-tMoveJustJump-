using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadLevelState : IPayloadState<string>
{
    private const string InitialPointTag = "InitialPoint";

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
        LevelStaticData levelData = LevelStaticData();

        GameObject player = InitPlayer(levelData);
        InitHud();
        InformProgressReaders();
        CameraFollow(player);
        _gameStateMachine.Enter<GameLoopState>();
        Debug.Log("OnLoad");

    }

    private LevelStaticData LevelStaticData()
    {
        string sceneKey = SceneManager.GetActiveScene().name;
        LevelStaticData levelData = _staticDataService.ForLevel(sceneKey);
        return levelData;
    }

    private GameObject InitPlayer(LevelStaticData levelData)
    {
        GameObject player = _entityFactory.CreatePlayer(levelData.InitialPlayerPoint);
        
        return player;
    }

    private void InitHud()
    {
        GameObject hud = _entityFactory.CreateHud();
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
