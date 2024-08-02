using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputSystem : InputService
{
    public override Vector2 Axis => SimpleInputAxis();

    public override bool IsJumpButtonUp() =>
        SimpleInput.GetButtonDown(Jump);

    protected override Vector2 SimpleInputAxis() =>
        new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

}
