using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState : IExitableState
{
    public void Enter();
}
