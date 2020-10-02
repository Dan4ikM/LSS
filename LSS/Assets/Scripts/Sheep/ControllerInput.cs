using UnityEngine;

public class ControllerInput : ISheepInput
{
    [SerializeField]
    private FloatingJoystick joystick;

    public void ReadInput()
    {
        if (joystick == null)
        {
            MoveDirection = Vector3.zero;
            return;
        }
        MoveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
    }

    public Vector3 MoveDirection { get; private set; }
}
