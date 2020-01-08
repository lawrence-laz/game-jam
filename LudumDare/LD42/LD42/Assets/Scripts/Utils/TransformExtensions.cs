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
}
