using UnityEngine;

public readonly struct GravitySurfaceData
{
    public readonly Vector2 ClosestPoint;
    public readonly Vector2 GravityDirection;
    public readonly Vector2 SurfaceNormal;
    public readonly Vector2 Tangent;

    public GravitySurfaceData(
        Vector2 closestPoint,
        Vector2 gravityDirection,
        Vector2 surfaceNormal,
        Vector2 tangent)
    {
        ClosestPoint = closestPoint;
        GravityDirection = gravityDirection;
        SurfaceNormal = surfaceNormal;
        Tangent = tangent;
    }
}
