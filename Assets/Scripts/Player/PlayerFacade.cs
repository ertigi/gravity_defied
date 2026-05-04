using UnityEngine;

public class PlayerFacade : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementController PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerGravityController PlayerGravity { get; private set; }
}
