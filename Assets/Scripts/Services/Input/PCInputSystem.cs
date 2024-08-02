using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputSystem : InputService
{
    public override Vector2 Axis => SimpleInputAxis();

    public override bool IsJumpButtonUp() =>
        Input.GetKeyDown(CodeJump);

    protected override Vector2 SimpleInputAxis() => 
        new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
}
