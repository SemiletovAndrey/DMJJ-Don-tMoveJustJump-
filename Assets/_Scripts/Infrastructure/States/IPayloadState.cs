using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPayloadState<TPayload> : IExitableState
{
    public void Enter(TPayload payload);
}
