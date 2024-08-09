using System;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public Canvas SettingsCanvas;
    public Action OnStartGameClicked;
    public Action OnSettingsClicked;

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
}
