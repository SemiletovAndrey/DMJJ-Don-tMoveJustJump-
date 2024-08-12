using System;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public Canvas SettingsCanvas;
    public Action OnStartGameClicked;
    public Action OnSettingsClicked;

    private void Start()
    {
        SettingsCanvas.gameObject.SetActive(false);
    }

    public void StartGameButton()
    {
        OnStartGameClicked?.Invoke();
    }

    public void SettingsButton()
    {
        OnSettingsClicked?.Invoke();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ApplyAndContinue()
    {
        ApplySettings();
        SettingsCanvas.gameObject.SetActive(false);
    }

    private void ApplySettings()
    {

    }
}
