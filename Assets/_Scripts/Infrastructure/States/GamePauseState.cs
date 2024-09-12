using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseState : IState
{
    public void Enter()
    {
        Time.timeScale = 0f;
    }

    public void Exit()
    {
        
    }
}
