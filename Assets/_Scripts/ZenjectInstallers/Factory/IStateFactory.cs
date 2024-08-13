public interface IStateFactory
{
    public BootstrapState CreateBootstrapState();
    public LoadProgressState CreateLoadProgressState();
    public LoadLevelState CreateLoadLevelState();
    public GameLoopState CreateGameLoopState();
    public MainMenuState CreateMainMenuState();
    public LoadSettingsState CreateLoadSettingsState();
}