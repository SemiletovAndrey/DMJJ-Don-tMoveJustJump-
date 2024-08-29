using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tools
{
    [MenuItem("Tools/Clear prefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        LevelStaticData scriptableObject = Resources.Load<LevelStaticData>($"StaticData/Levels/{SceneManager.GetActiveScene().name}");
        scriptableObject.CurrentCheckpointIndex = 0;
        EditorUtility.SetDirty(scriptableObject);
        AssetDatabase.SaveAssets();
    }
}
