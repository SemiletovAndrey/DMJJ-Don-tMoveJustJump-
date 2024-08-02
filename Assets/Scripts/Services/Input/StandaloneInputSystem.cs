
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneInputSystem : InputService
{

    public override Vector2 Axis {
        get
        {
            Vector2 axis = SimpleInputAxis();
            if (axis == Vector2.zero)
            {
                UnityAxis();
            }
            return axis;
        }
    }

    public override bool IsJumpButtonUp()
    {
        if (SimpleInput.GetButtonDown(Jump))
        {
            return true;
        }
        else if (UnityEngine.Input.GetKeyDown(CodeJump))
        {
            return true;
        }
        return false;

    }

    protected override Vector2 SimpleInputAxis() =>
        new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

    private static Vector2 UnityAxis() =>
           new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

}
