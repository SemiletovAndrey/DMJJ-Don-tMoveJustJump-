using UnityEngine;

public class SaveLoadProgressService : ISaveProgressService
{
    private const string ProgressKey = "Progress";

    private readonly IEntityFactory _gameFactory;
    private readonly IPersistantProgressService _persistantProgress;

    public SaveLoadProgressService(IPersistantProgressService persistantProgress, IEntityFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _persistantProgress = persistantProgress;
    }

    public PlayerProgress LoadProgress()=>
        PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    

    public void SaveProgress()
    {
        foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriter)
        {
            progressWriter.UpdateProgress(_persistantProgress.Progress);
        }

        PlayerPrefs.SetString(ProgressKey, _persistantProgress.Progress.ToJson());
    }
}
