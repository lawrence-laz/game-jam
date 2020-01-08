using System.Collections.Generic;
using UnityEngine;

namespace UnityGoodies
{
    public static class CollidersExtensions
    {
        public static void SetEnabled(this IEnumerable<Collider> colliders, bool value)
        {
            if (colliders == null)
                return;

            foreach (var collider in colliders)
            {
                collider.enabled = value;
            }
        }
    }
}
