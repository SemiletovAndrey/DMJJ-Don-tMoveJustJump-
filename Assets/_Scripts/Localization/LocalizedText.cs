using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string key;

    [Inject] private LocalizationManager localizationManager;
    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        localizationManager.RegisterText(this);
            
    }

    void Start()
    {
        UpdateText();
    }

    private void OnDestroy()
    {
        localizationManager.UnregisterText(this);
    }

    public void UpdateText()
    {
        text.text = localizationManager.GetLocalizedValue(key);
    }
}
