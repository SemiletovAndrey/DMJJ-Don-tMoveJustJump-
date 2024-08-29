using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader 
{
    private readonly ICoroutineRunner _coroutineRunner;

    [Inject]
    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public void Load(string nameScene, Action onLoaded = null)
    {
        _coroutineRunner.StartCoroutine(LoadScene(nameScene, onLoaded));
    }
    public void LoadForRestart(string nameScene, Action onLoaded = null)
    {
        _coroutineRunner.StartCoroutine(LoadSceneForRestart(nameScene, onLoaded));
    }

    private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
        if (SceneStaticService.CurrentLevel() == nextScene)
        {
            onLoaded?.Invoke();
            yield break;
        }

        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
        while (!waitNextScene.isDone)
            yield return null;
        onLoaded?.Invoke();

    }
    
    
    private IEnumerator LoadSceneForRestart(string nextScene, Action onLoaded = null)
    {
        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
        while (!waitNextScene.isDone)
            yield return null;
        onLoaded?.Invoke();

    }
}
