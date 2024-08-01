using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInputServices();
    }

    private void BindInputServices()
    {
        if (Application.isEditor)
        {
            Container.Bind<IInputService>().To<StandaloneInputSystem>().AsSingle();
        }
        else if (Application.isMobilePlatform)
        {
            Container.Bind<IInputService>().To<MobileInputSystem>().AsSingle();
        }
        else
        {
            Container.Bind<IInputService>().To<PCInputSystem>().AsSingle();
        }
    }
}
