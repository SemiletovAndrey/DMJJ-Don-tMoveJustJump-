using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StateFactory : IStateFactory
{
    private readonly DiContainer _container;

    public StateFactory(DiContainer container)
    {
        Debug.Log("StateFactoryConstruct");
        _container = container;
    }

    public BootstrapState CreateBootstrapState()
    {
        return _container.Instantiate<BootstrapState>();
    }

    public GameLoopState CreateGameLoopState()
    {
        return _container.Instantiate<GameLoopState>();
    }

    public LoadLevelState CreateLoadLevelState()
    {
        return _container.Instantiate<LoadLevelState>();
    }

    public LoadProgressState CreateLoadProgressState()
    {
        return _container.Instantiate<LoadProgressState>();
    }
    
    public MainMenuState CreateMainMenuState()
    {
        return _container.Instantiate<MainMenuState>();
    }

}
