using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GravitySource2D : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    private void Reset()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public bool TryGetClosestSurface(Vector2 actorPosition, out GravitySurfaceData data)
    {
        Vector2 center = _collider.bounds.center;

        Vector2 rayDirection = center - actorPosition;
        float rayDistance = rayDirection.magnitude;

        if (rayDistance <= 0.0001f)
        {
            data = default;
            return false;
        }

        rayDirection /= rayDistance;

        RaycastHit2D hit = Physics2D.Raycast(actorPosition, rayDirection, rayDistance, 1 << 7);

        if (hit.collider == null)
        {
            data = default;
            return false;
        }

        Vector2 closestPoint = hit.point;

        Vector2 surfaceNormal = GetBoxSurfaceNormal(hit.point);

        Vector2 gravityDirection = -surfaceNormal;

        Vector2 tangent = new Vector2(-surfaceNormal.y, surfaceNormal.x).normalized;

        data = new GravitySurfaceData(
            closestPoint,
            gravityDirection,
            surfaceNormal,
            tangent
        );

        return true;
    }

    private Vector2 GetBoxSurfaceNormal(Vector2 worldPoint)
    {
        Vector2 localPoint = transform.InverseTransformPoint(worldPoint);

        BoxCollider2D box = _collider;

        Vector2 halfSize = box.size * 0.5f;
        Vector2 offset = box.offset;

        Vector2 local = localPoint - offset;

        float dx = halfSize.x - Mathf.Abs(local.x);
        float dy = halfSize.y - Mathf.Abs(local.y);

        Vector2 localNormal;

        if (dx < dy)
            localNormal = local.x > 0f ? Vector2.right : Vector2.left;
        else
            localNormal = local.y > 0f ? Vector2.up : Vector2.down;

        return transform.TransformDirection(localNormal).normalized;
    }
}
