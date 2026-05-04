using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private MobilePlayerInput _input;
    [SerializeField] private InpytButtonType _buttonType;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_buttonType)
        {
            case InpytButtonType.MoveLeft:
                _input.SetMoveLeft(true);
                break;

            case InpytButtonType.MoveRight:
                _input.SetMoveRight(true);
                break;

            case InpytButtonType.Jump:
                _input.PressJump();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (_buttonType)
        {
            case InpytButtonType.MoveLeft:
                _input.SetMoveLeft(false);
                break;

            case InpytButtonType.MoveRight:
                _input.SetMoveRight(false);
                break;
        }
    }
}
