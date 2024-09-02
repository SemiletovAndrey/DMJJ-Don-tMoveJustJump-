using UnityEngine;

public class MobileInputSystem : InputService
{
    public override Vector2 Axis => SimpleInputAxis();

    public override bool IsJumpButtonUp() =>
        SimpleInput.GetButtonDown(Jump);

    public override bool IsTurnLeftCameraButton() => 
        SimpleInput.GetButton(TurnRightCamera);

    public override bool IsTurnRightCameraButton() => 
        SimpleInput.GetButton(TurnLeftCamera);

    public override bool IsResetCameraButton() => 
        SimpleInput.GetButton(ResetCamera);
    public override bool IsMenuPause() => 
        SimpleInput.GetButton(Pause);

    protected override Vector2 SimpleInputAxis() =>
        new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

}
