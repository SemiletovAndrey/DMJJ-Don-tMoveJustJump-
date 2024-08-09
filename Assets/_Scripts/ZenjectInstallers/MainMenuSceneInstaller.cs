using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private Canvas settingsCanvas;
    public override void InstallBindings()
    {
        Container.Bind<MainMenuUI>().FromInstance(mainMenuUI).AsSingle();
        Container.Bind<Canvas>().FromInstance(settingsCanvas).AsSingle();
    }
}
