using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneStaticService
{
    public static string CurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }
}
