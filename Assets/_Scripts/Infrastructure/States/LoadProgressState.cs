using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProgressState : IState
{
    public LoadProgressState()
    {
        
    }

    public void Enter()
    {
        Debug.Log("Enter LoadProgress State");
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
