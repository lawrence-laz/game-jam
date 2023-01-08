using UnityEngine;

public static class TransformExtensions
{
    public static void LookAt2D(this Transform transform, Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);
    }

    public static void LookTowards2D(this Transform transform, Vector3 target, float maxDegreesDelta)
    {
        Vector3 direction = (target - transform.position).normalized;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotation - 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreesDelta);
    }

    public static float DistanceTo(this Transform a, Transform b)
    {
        return (a.position - b.position).magnitude;
    }

    public static float DistanceTo2D(this Transform a, Transform b)
    {
        return ((Vector2)(a.position - b.position)).magnitude;
    }

    public static float DistanceTo2D(this Transform a, Vector3 b)
    {
        return ((Vector2)(a.position - b)).magnitude;
    }

    public static float DistanceTo2D(this Vector3 a, Transform b)
    {
        return ((Vector2)(a - b.position)).magnitude;
    }

    public static float DistanceTo2D(this Vector3 a, Vector3 b)
    {
        return ((Vector2)(a - b)).magnitude;
    }

    public static Vector3 DirectionTo(this Transform from, Transform to)
    {
        return (to.position - from.position).normalized;
    }

    public static Vector3 DirectionTo2D(this Transform from, Transform to)
    {
        return ((Vector2)(to.position - from.position)).normalized;
    }

    public static Transform SetLocalScaleY(this Transform transform, float y)
    {
        var scale = transform.localScale;
        scale.y = y;
        transform.localScale = scale;

        return transform;
    }
}
