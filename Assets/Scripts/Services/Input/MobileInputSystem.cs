using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputSystem : InputService
{
    public override Vector2 Axis => SimpleInputAxis();

    public override bool IsJumpButtonUp() =>
        SimpleInput.GetButtonDown(Jump);

    public override bool IsTurnLeftCameraButton() => 
        SimpleInput.GetButton(TurnLeftCamera);

    public override bool IsTurnRightCameraButton() => 
        SimpleInput.GetButton(TurnRightCamera);

    public override bool IsResetCameraButton()
    {
        return false;
    }

    protected override Vector2 SimpleInputAxis() =>
        new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

}
