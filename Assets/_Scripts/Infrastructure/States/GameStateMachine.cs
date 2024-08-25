using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateMachine : IGameStateMachine, IInitializable
{
    private readonly Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
    private IExitableState _activeState;
    private IStateFactory _stateFactory;

    public GameStateMachine(IStateFactory stateFactory)
    {
        _stateFactory = stateFactory;
    }

    public void InitializeStates()
    {
        _states[typeof(BootstrapState)] = _stateFactory.CreateBootstrapState();
        _states[typeof(LoadSettingsState)] = _stateFactory.CreateLoadSettingsState();
        _states[typeof(LoadProgressState)] = _stateFactory.CreateLoadProgressState();
        _states[typeof(LoadLevelState)] = _stateFactory.CreateLoadLevelState();
        _states[typeof(MainMenuState)] = _stateFactory.CreateMainMenuState();
        _states[typeof(GameLoopState)] = _stateFactory.CreateGameLoopState();
    }

    public void Initialize()
    {
        InitializeStates();
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
        TState state = ChangeState<TState>();

        state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();

        TState state = GetState<TState>();
        _activeState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState
    {
        return _states[typeof(TState)] as TState;
    }

    
}
