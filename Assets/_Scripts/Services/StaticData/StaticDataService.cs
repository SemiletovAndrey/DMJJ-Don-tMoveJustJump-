using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticDataService : IStaticDataService
{
    private Dictionary<string, LevelStaticData> _levels;

    public void Load()
    {
        _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
            .ToDictionary(x => x.LevelKey, x => x);
    }

    public LevelStaticData ForLevel(string sceneKey)
    {
        _levels.TryGetValue(sceneKey, out LevelStaticData staticData);
        if (staticData)
        {
            return staticData;
        }
        else
        {
            return null;
        }
    }

    public LevelStaticData GetLevelStaticData()
    {
        string sceneKey = SceneStaticService.CurrentLevel();
        LevelStaticData levelData = this.ForLevel(sceneKey);
        return levelData;
    }
}
