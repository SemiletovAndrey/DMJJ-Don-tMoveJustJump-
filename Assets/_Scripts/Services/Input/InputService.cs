using UnityEngine;

public abstract class InputService : IInputService
{
    protected const string Jump = "Jump";
    protected const string Vertical = "Vertical";
    protected const string Horizontal = "Horizontal";
    protected const string TurnRightCamera = "TurnRightCamera";
    protected const string TurnLeftCamera = "TurnLeftCamera";
    protected const string ResetCamera = "ResetCamera";
    protected const string Pause = "Pause";

    protected KeyCode CodeJump = KeyCode.Space;
    protected KeyCode CodeTurnLeftCamera = KeyCode.Q;
    protected KeyCode CodeTurnRightCamera = KeyCode.E;
    protected KeyCode CodeResetCamera = KeyCode.R;
    protected KeyCode CodePause = KeyCode.Escape;


    public abstract Vector2 Axis { get; }

    public abstract bool IsJumpButtonUp();
    public abstract bool IsTurnLeftCameraButton();
    public abstract bool IsTurnRightCameraButton();
    public abstract bool IsResetCameraButton();
    public abstract bool IsMenuPause();

    protected abstract Vector2 SimpleInputAxis();

    
}
