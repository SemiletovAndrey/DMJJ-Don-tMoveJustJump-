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

        Container.Bind<IPersistantProgressService>().To<PersistantProgressService>().AsSingle();
        Container.Bind<ISaveSettingsService>().To<SaveLoadSettingService>().AsSingle();
        Container.Bind<ISaveProgressService>().To<SaveLoadProgressService>().AsSingle();
        Container.Bind<SettingsData>().AsSingle();
        
        Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
    }

    private void BindingSettingsData()
    {
        Container.Bind<SettingsData>().FromMethod(context =>
        {
            ISaveSettingsService saveSettingsService = context.Container.Resolve<ISaveSettingsService>();
            SettingsData loadedData = saveSettingsService.LoadSettings();
            if (loadedData == null)
            {
                return new SettingsData
                {
                    Language = LanguageEnum.English,
                    MusicVolume = 0.5f,
                    SoundVolume = 0.5f,
                    Sensitivity = 100f,
                    GraphicsSettings = GraphicsSettingsEnum.Low,
                };
            }
            else
            {
                return new SettingsData
                {
                    Language = loadedData.Language,
                    SoundVolume = loadedData.SoundVolume,
                    MusicVolume = loadedData.MusicVolume,
                    Sensitivity = loadedData.Sensitivity,
                    GraphicsSettings = loadedData.GraphicsSettings
                };
            }

        }).AsSingle();
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
