using Zenject;

public class BootstrapState : IState
{
    private const string Initial = "Init";
    private const string MainMenuName = "MainMenuScene";

    private readonly IGameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IStaticDataService _staticDataService;

    [Inject]
    public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, IStaticDataService staticDataService)
    {
        this._gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _staticDataService = staticDataService;
    }

    public void Enter()
    {
        _staticDataService.Load();
        _sceneLoader.Load(Initial, EnterLevelLoad);
    }

    private void EnterLevelLoad()
    {
        _sceneLoader.Load(MainMenuName, OnLoad);

    }

    private void OnLoad()
    {
        _gameStateMachine.Enter<LoadSettingsState>();
    }

    public void Exit()
    {

    }
}
