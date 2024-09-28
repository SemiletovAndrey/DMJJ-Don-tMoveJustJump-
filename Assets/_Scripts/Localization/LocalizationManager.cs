using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class LocalizationManager : MonoBehaviour
{
    private string _currentLanguage;
    private Dictionary<string, string> localizedText;
    private static bool isReady = false;
    public event Action OnLanguageChanged;
    private List<LocalizedText> localizedTexts = new List<LocalizedText>();


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

        string dataAsJson = string.Empty;

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest request = UnityWebRequest.Get(path);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                dataAsJson = request.downloadHandler.text;
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
                dataAsJson = File.ReadAllText(path);
            }
            else
            {
                Debug.LogError("Localization file not found: " + path);
                yield break;
            }
        }

        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.Items.Length; i++)
        {
            localizedText.Add(loadedData.Items[i].Key, loadedData.Items[i].Value);
        }
        Debug.Log($"Settings data language {_currentLanguage}");

        isReady = true;
        OnLanguageChanged?.Invoke();
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
