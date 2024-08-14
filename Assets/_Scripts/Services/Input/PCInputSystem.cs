using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputSystem : InputService
{
    public override Vector2 Axis => SimpleInputAxis();

    public override bool IsJumpButtonUp() =>
        Input.GetKeyDown(CodeJump);

    public override bool IsTurnLeftCameraButton() => 
        Input.GetKey(CodeTurnLeftCamera);

    public override bool IsTurnRightCameraButton() => 
        Input.GetKey(CodeTurnRightCamera);

    public override bool IsResetCameraButton() => 
        Input.GetKeyDown(CodeResetCamera);
    public override bool IsMenuPause() => 
        Input.GetKeyDown(CodePause);

    protected override Vector2 SimpleInputAxis() => 
        new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

}
