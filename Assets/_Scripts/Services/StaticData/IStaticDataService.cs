public interface IStaticDataService
{
    LevelStaticData ForLevel(string sceneKey);
    void Load();
    public LevelStaticData GetLevelStaticData();
}