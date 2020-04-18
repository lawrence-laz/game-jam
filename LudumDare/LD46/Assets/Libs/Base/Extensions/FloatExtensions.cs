using UnityEngine;

namespace Libs.Base.Extensions
{
    public static class FloatExtensions
    {
        public static float Abs(this float x)
        {
            return Mathf.Abs(x);
        }

        public static float Sign(this float x)
        {
            return Mathf.Sign(x);
        }

        /// <summary>
        /// If limit is negative, return current.
        /// </summary>
        public static float UpperLimit(this float current, float limit)
        {
            if (limit < 0)
                return current;

            return current.Sign() * Mathf.Min(current.Abs(), limit);
        }
    }
}
