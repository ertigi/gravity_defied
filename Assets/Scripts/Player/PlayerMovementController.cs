using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private PlayerGravityController _gravityController;
    private PlayerMovementConfig _config;
    private NearestGravitySourceProvider _gravityProvider;
    private IPlayerInput _input;

    public void Init(IPlayerInput playerInput, PlayerMovementConfig config, NearestGravitySourceProvider nearestGravitySourceProvider, PlayerGravityController playerGravityController)
    {
        _input = playerInput;
        _config = config;
        _gravityProvider = nearestGravitySourceProvider;
        _gravityController = playerGravityController;
    }

    private void FixedUpdate()
    {
        Move();

        if (_input.ConsumeJumpPressed() && _gravityController.IsGrounded)
            Jump();
    }

    private void Move()
    {
        Vector2 tangent = _gravityProvider.SurfaceData.Tangent;
        Vector2 normal = _gravityProvider.SurfaceData.SurfaceNormal;

        float moveAxis = _input.MoveAxis;

        Vector2 velocity = _rigidbody.linearVelocity;

        float normalSpeed = Vector2.Dot(velocity, normal);
        Vector2 targetTangentVelocity = tangent * (moveAxis * _config.MoveSpeed);
        Vector2 preservedNormalVelocity = normal * normalSpeed;

        _rigidbody.linearVelocity = targetTangentVelocity + preservedNormalVelocity;
    }

    private void Jump()
    {
        Vector2 normal = _gravityProvider.SurfaceData.SurfaceNormal;

        Vector2 velocity = _rigidbody.linearVelocity;

        float inwardSpeed = Vector2.Dot(velocity, normal);

        _rigidbody.linearVelocity = velocity;
        _rigidbody.AddForce(normal * _config.JumpForce, ForceMode2D.Impulse);
    }
}
