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
        else if (Input.GetKeyDown(CodeJump))
        {
            return true;
        }
        return false;

    }

    public override bool IsTurnLeftCameraButton()
    {
        if (SimpleInput.GetButton(TurnLeftCamera))
        {
            return true;
        }
        else if (Input.GetKey(CodeTurnRightCamera))
        {
            return true;
        }
        return false;
    }

    public override bool IsTurnRightCameraButton()
    {
        if (SimpleInput.GetButton(TurnRightCamera))
        {
            return true;
        }
        else if (Input.GetKey(CodeTurnLeftCamera))
        {
            return true;
        }
        return false;
    }

    public override bool IsResetCameraButton()
    {
        if (Input.GetKey(CodeResetCamera))
        {
            return true;
        }
        return false;
    }

    protected override Vector2 SimpleInputAxis() =>
        new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

    private static Vector2 UnityAxis() =>
           new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
}
