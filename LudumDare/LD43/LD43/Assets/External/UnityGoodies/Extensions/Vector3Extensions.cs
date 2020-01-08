using UnityEngine;

public static class Vector3Extensions
{
    public static float DistanceTo(this Vector3 start, Vector3 end)
    {
        return (end - start).magnitude;
    }

    public static Vector3 DirectionTo(this Vector3 start, Vector3 end)
    {
        return (end - start).normalized;
    }
}
