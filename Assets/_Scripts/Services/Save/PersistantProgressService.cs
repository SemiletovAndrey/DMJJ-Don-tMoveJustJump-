public class PersistantProgressService : IPersistantProgressService
{
    public PlayerProgress Progress { get; set; }
    public PersistantProgressService()
    {
        Progress = new PlayerProgress();
    }
}
