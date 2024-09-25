using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LanguageDropdown : MonoBehaviour
{
    [Inject] private LocalizationManager localizationManager;
    [SerializeField] private TMP_Dropdown languageDropdown;

    private void Start()
    {
        languageDropdown.onValueChanged.AddListener(OnLanguageSelected);
    }

    public void OnLanguageSelected(int index)
    {
        string selectedLanguage = languageDropdown.options[index].text;
        Debug.Log("Selected Language: " + selectedLanguage);
        switch(selectedLanguage)
        {
            case "English":
                localizationManager.CurrentLanguage = "en_US";
                break;
            case "Русский":
                localizationManager.CurrentLanguage = "ru_RU";
                break;
            case "Українська":
                localizationManager.CurrentLanguage = "ua_UA";
                break;
            default:
                localizationManager.CurrentLanguage = "en_US";
                break;
        }
    }
}
