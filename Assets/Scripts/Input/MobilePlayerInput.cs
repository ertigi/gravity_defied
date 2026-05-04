using UnityEngine;

public class MobilePlayerInput : MonoBehaviour, IPlayerInput
{
    private bool _moveLeft;
    private bool _moveRight;
    private bool _jumpPressed;

    public float MoveAxis
    {
        get
        {
            if (_moveLeft && !_moveRight) return 1f;
            if (_moveRight && !_moveLeft) return -1f;
            return 0f;
        }
    }

    public void SetMoveLeft(bool value) => _moveLeft = value;
    public void SetMoveRight(bool value) => _moveRight = value;
    public void PressJump() => _jumpPressed = true;

    public bool ConsumeJumpPressed()
    {
        if (!_jumpPressed)
            return false;

        _jumpPressed = false;
        return true;
    }
}