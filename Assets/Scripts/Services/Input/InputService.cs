using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputService : IInputService
{
    protected const string Jump = "Jump";
    protected const string Vertical = "Vertical";
    protected const string Horizontal = "Horizontal";

    protected KeyCode CodeJump = KeyCode.Space;


    public abstract Vector2 Axis { get; }

    public abstract bool IsJumpButtonUp();

    protected abstract Vector2 SimpleInputAxis();
}
