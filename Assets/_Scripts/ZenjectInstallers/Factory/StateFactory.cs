using UnityEngine;
using Zenject;

public class StateFactory : IStateFactory
{
    private readonly DiContainer _container;

    public StateFactory(DiContainer container)
    {
        _container = container;
    }

    public T CreateTState<T>() where T : IExitableState
    {
        return _container.Instantiate<T>();
    }
}
