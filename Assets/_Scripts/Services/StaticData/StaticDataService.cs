using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            Debug.Log("Yes");
            return staticData;
        }
        else
        {
            Debug.Log("No");
            return null;
        }
    }
}
