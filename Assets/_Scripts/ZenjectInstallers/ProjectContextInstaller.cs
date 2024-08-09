using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller, ICoroutineRunner
{
    [SerializeField] private LoadingCurtain loadingCurtain;
    public CharacterSettings characterSettings;

    public override void InstallBindings()
    {
        BindInputServices();
        GameStateMachineBindings();
        EntityFactoryBindings();

        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
    }

    private void GameStateMachineBindings()
    {
        Container.Bind<SceneLoader>().FromNew().AsSingle().WithArguments(this).NonLazy();
        Container.Bind<IStateFactory>().To<StateFactory>().AsSingle().NonLazy();
        Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle().OnInstantiated<GameStateMachine>((context, machine) => machine.InitializeStates());
        Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(loadingCurtain).AsSingle();
    }

    private void EntityFactoryBindings()
    {
        Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();
        Container.Bind<CharacterSettings>().FromInstance(characterSettings).AsSingle();
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
