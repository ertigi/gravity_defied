using UnityEngine;

public class NearestGravitySourceProvider
{
    public GravitySurfaceData SurfaceData { get; private set; }

    private readonly GravitySource2D[] _sources;

    public NearestGravitySourceProvider(GravitySource2D[] gravitySources)
    {
        _sources = gravitySources;
    }

    public void FindSurfaceData(Vector2 actorPosition)
    {
        GravitySurfaceData bestData = default;
        float bestDistanceSqr = float.MaxValue;
        bool hasSource = false;

        for (int i = 0; i < _sources.Length; i++)
        {
            GravitySource2D source = _sources[i];

            if (source == null)
                continue;

            if (!source.TryGetClosestSurface(actorPosition, out GravitySurfaceData data))
                continue;

            float distanceSqr = (data.ClosestPoint - actorPosition).sqrMagnitude;

            if (distanceSqr < bestDistanceSqr)
            {
                bestDistanceSqr = distanceSqr;
                bestData = data;
                hasSource = true;
            }
        }

        if (!hasSource)
            bestData =  CreateFallback(actorPosition);

        SurfaceData = bestData;
    }

    private GravitySurfaceData CreateFallback(Vector2 actorPosition)
    {
        Vector2 gravityDirection = Vector2.down;
        Vector2 surfaceNormal = Vector2.up;
        Vector2 closestPoint = actorPosition + gravityDirection;
        Vector2 tangent = Vector2.right;

        return new GravitySurfaceData(
            closestPoint,
            gravityDirection,
            surfaceNormal,
            tangent
        );
    }
}