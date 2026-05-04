using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerGravityController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private NearestGravitySourceProvider _gravityProvider;
    private PlayerMovementConfig _config;

    public bool IsGrounded { get; private set; }

    public void Init(NearestGravitySourceProvider gravityProvider, PlayerMovementConfig config)
    {
        _gravityProvider = gravityProvider;
        _config = config;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        _gravityProvider.FindSurfaceData(_rigidbody.position);

        ApplyGravity();
        CheckGrounded();
        RotateToSurface();
    }

    private void ApplyGravity()
    {
        _rigidbody.AddForce(_gravityProvider.SurfaceData.GravityDirection * _config.GravityForce, ForceMode2D.Force);
    }

    private void CheckGrounded()
    {
        Vector2 origin = _rigidbody.position;

        Vector2 direction = _gravityProvider.SurfaceData.GravityDirection;

        float distance = _config.GroundCheckDistance;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction,
            distance,
            1 << 7
        );

        IsGrounded = hit.collider != null;

    }

    private void RotateToSurface()
    {
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, _gravityProvider.SurfaceData.SurfaceNormal) * transform.rotation;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            _config.RotationSpeed * Time.fixedDeltaTime
        );
    }
}
