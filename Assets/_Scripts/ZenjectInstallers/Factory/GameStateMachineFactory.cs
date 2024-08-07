using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateMachineFactory : IGameStateMachineFactory
{
    private readonly DiContainer _container;

    public GameStateMachineFactory(DiContainer container)
    {
        _container = container;
    }

    public IGameStateMachine Create()
    {
        return _container.Instantiate<GameStateMachine>();
    }
}
