using UnityEngine;

public class KeyboardPlayerInput : MonoBehaviour, IPlayerInput
{
    public float MoveAxis { get; private set; }
    private bool _jumpPressed;

    private void Update()
    {
        MoveAxis = 0f;

        if (Input.GetKey(KeyCode.A))
            MoveAxis = 1f;

        if (Input.GetKey(KeyCode.D))
            MoveAxis = -1f;

        if (Input.GetKeyDown(KeyCode.Space))
            _jumpPressed = true;
    }

    public bool ConsumeJumpPressed()
    {
        if (!_jumpPressed)
            return false;

        _jumpPressed = false;
        return true;
    }
}
