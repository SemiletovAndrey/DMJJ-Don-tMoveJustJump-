using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopState : IState
{
    public GameLoopState(IGameStateMachine gameStateMachine)
    {

    }
    public void Enter()
    {
        Time.timeScale = 1f;
    }

    public void Exit()
    {

    }
}
