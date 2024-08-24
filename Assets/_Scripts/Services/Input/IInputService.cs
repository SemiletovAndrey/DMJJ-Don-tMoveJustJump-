using UnityEngine;

public interface IInputService
{
    public Vector2 Axis { get; }
    public bool IsJumpButtonUp();
    public bool IsTurnLeftCameraButton();
    public bool IsTurnRightCameraButton();
    public bool IsResetCameraButton();
    public bool IsMenuPause();
}
