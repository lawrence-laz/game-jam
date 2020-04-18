using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static IEnumerable<Vector2> Around(this Transform transform)
    {
        yield return (Vector2)transform.position + Vector2.up;
        yield return (Vector2)transform.position + Vector2.down;
        yield return (Vector2)transform.position + Vector2.left;
        yield return (Vector2)transform.position + Vector2.right;
    }

    public static bool TryGetComponentSafe<T>(this GameObject gameObject, out T component)
    {
        if (gameObject == null)
        {
            component = default;
            return false;
        }

        component = gameObject.GetComponent<T>();
        return component != null;
    }

    public static IEnumerable<T> GetComponents<T>(this IEnumerable<GameObject> gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            var component = gameObject.GetComponent<T>();
            if (component != null)
            {
                yield return component;
            }
        }
    }

    public static bool TryGet<T>(this IEnumerable<GameObject> gameObjects, out T component)
    {
        if (!gameObjects.Any())
        {
            component = default;
            return false;
        }

        foreach (var gameObject in gameObjects)
        {
            if (gameObject.TryGetComponentSafe<T>(out component))
            {
                return true;
            }
        }

        component = default;

        return false;
    }

    public static Vector2[] LineTo(this Vector3 start, Vector3 end, bool diagonalAllowed = false) => ((Vector2)start).LineTo(end, diagonalAllowed);

    public static Vector2[] LineTo(this Vector2 start, Vector2 end, bool diagonalAllowed = false)
    {
        var points = new List<Vector2>();
        var dx = Mathf.Abs((int)end.x - (int)start.x);
        var dy = -Mathf.Abs((int)end.y - (int)start.y);
        var sx = start.x < end.x ? 1 : -1;
        var sy = start.y < end.y ? 1 : -1;

        var error = dx + dy;
        int e2;

        while (true)
        {
            points.Add(start);

            e2 = error * 2;

            if (diagonalAllowed)
            {
                if (e2 >= dy)
                {
                    if (start.x == end.x)
                    {
                        break;
                    }
                    error += dy;
                    start.x += sx;
                }

                if (e2 <= dx)
                {
                    if (start.y == end.y)
                    {
                        break;
                    }

                    error += dx;
                    start.y += sy;
                }
            }
            else
            {
                if (e2 - dy > dx - e2)
                {
                    if (start.x == end.x)
                    {
                        break;
                    }
                    error += dy;
                    start.x += sx;
                }
                else
                {
                    if (start.y == end.y)
                    {
                        break;
                    }

                    error += dx;
                    start.y += sy;
                }
            }

        }

        return points.ToArray();
    }
}
