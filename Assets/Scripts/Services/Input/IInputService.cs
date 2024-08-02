using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputService
{
    public Vector2 Axis { get; }
    public bool IsJumpButtonUp();
}
