using UnityEngine;

namespace UnityGoodies
{
    public static class Vector2IntExtensions
    {
        public static Vector3 ToVector3(this Vector2Int vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }

        public static Vector2Int MoveTowards(this Vector2Int current, Vector2Int target, int step)
        {
            return new Vector2Int((int)Mathf.MoveTowards(current.x, target.x, step), (int)Mathf.MoveTowards(current.y, target.y, step));
        }
    }
}
