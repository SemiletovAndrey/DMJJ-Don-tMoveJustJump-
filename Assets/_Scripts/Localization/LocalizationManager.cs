using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class LocalizationManager : MonoBehaviour
{
    private string _currentLanguage;
    private string _dataAsJson;
    private Dictionary<string, string> localizedText;
    private static bool isReady = false;
    public event Action OnLanguageChanged;
    private List<LocalizedText> localizedTexts = new List<LocalizedText>();

    private DialogueScene currentSceneDialogues;


    public string CurrentLanguage
    {
        get { return _currentLanguage; }
        set
        {
            if (_currentLanguage != value)
            {
                _currentLanguage = value;
                LoadLocalizedText(_currentLanguage);
            }
        }
    }
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }

    public void LoadLocalizedText(string langName)
    {
        StartCoroutine(LoadLocalizedTextCoroutine(langName));
        UpdateAllTexts();
    }

    private IEnumerator LoadLocalizedTextCoroutine(string langName)
    {
        Debug.Log($"{langName}");
        string path = $"{Application.streamingAssetsPath}/Languages/{langName}.json";

        _dataAsJson = string.Empty;

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest request = UnityWebRequest.Get(path);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                _dataAsJson = request.downloadHandler.text;
            }
            else
            {
                Debug.LogError("Error loading localization file: " + request.error);
                yield break;
            }
        }
        else
        {
            if (File.Exists(path))
            {
                _dataAsJson = File.ReadAllText(path);
            }
            else
            {
                Debug.LogError("Localization file not found: " + path);
                yield break;
            }
        }

        LocalizedUIText(_dataAsJson);


        isReady = true;
        OnLanguageChanged?.Invoke();
    }

    private void LocalizedUIText(string dataAsJson)
    {
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.Items.Length; i++)
        {
            localizedText.Add(loadedData.Items[i].Key, loadedData.Items[i].Value);
        }
        Debug.Log($"Settings data language {_currentLanguage}");
    }

    private void LoadCurrentSceneDialogues(string dataAsJson)
    {
        string sceneName = SceneStaticService.CurrentLevel();
        DialogueData dialogueData = JsonConvert.DeserializeObject<DialogueData>(dataAsJson);
        if (dialogueData.DialogueScene.TryGetValue(sceneName, out DialogueScene dialogScene))
        {
            currentSceneDialogues = dialogScene;
            Debug.Log($"Loaded dialogues for scene: {sceneName}");
        }
        else
        {
            Debug.LogWarning($"No dialogues found for scene: {sceneName}");
            currentSceneDialogues = null;
        }
    }

    [ContextMenu("Load dialog")]
    public void LoadCurrentSceneDialogues()
    {
        LoadCurrentSceneDialogues(_dataAsJson);
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            throw new Exception("Localized text with key \"" + key + "\" not found");
        }
    }

    public string GetDialoguePhrase(string characterKeyName, string phraseKey)
    {
        if (currentSceneDialogues == null)
        {
            Debug.LogError("No dialogues loaded for the current scene.");
            return string.Empty;
        }

        if (currentSceneDialogues.Dialogues.TryGetValue(characterKeyName, out DialogueItem dialogueItem))
        {
            if (dialogueItem.Phrases.TryGetValue(phraseKey, out string dialogueText))
            {
                return dialogueText;
            }
            else
            {
                Debug.LogError($"Phrase \"{phraseKey}\" not found for character \"{characterKeyName}\".");
            }
        }
        else
        {
            Debug.LogError($"Character \"{characterKeyName}\" not found.");
        }

        return string.Empty;
    }

    public void RegisterText(LocalizedText text)
    {
        if (!localizedTexts.Contains(text))
        {
            localizedTexts.Add(text);
        }
    }

    public void UnregisterText(LocalizedText text)
    {
        localizedTexts.Remove(text);
    }

    private void UpdateAllTexts()
    {
        foreach (var text in localizedTexts)
        {
            text.UpdateText();
        }
    }

}
