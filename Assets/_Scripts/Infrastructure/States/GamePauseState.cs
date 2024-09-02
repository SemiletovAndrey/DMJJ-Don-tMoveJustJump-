using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseState : IState
{
    public void Enter()
    {
        Debug.Log("Enter gamePause");
    }

    public void Exit()
    {
        Debug.Log("Exit gamePause");
    }
}
