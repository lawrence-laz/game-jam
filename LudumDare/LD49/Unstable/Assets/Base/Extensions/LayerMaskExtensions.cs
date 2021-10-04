using UnityEngine;

namespace Libs.Base.Extensions
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        public static bool Contains(this LayerMask mask, GameObject gameObject)
        {
            return mask.Contains(gameObject.layer);
        }
    }
}