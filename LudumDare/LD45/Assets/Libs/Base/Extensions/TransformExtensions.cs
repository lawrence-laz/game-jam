using UnityEngine;

namespace Libs.Base.Extensions
{
    public static class TransformExtensions
    {
        public static Transform FlipHorizontal(this Transform transform, int? direction = null)
        {
            var localScale = transform.localScale;
            if (direction.HasValue && Mathf.Sign(localScale.x) != Mathf.Sign(direction.Value))
                localScale.x *= -1;
            else
                localScale.x *= -1;
            transform.localScale = localScale;

            return transform;
        }
        
        public static void LookAt2D(this Transform transform, Vector3 target)
        {
            Vector3 direction = (target - transform.position);
            direction.y = 0;
            direction = direction.normalized;
            direction.y = direction.z;
            direction.z = 0;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, rotation - 90);
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
        
        public static Transform SetScaleX(this Transform transform, float value)
        {
            var scale = transform.localScale;
            scale.x = value;
            transform.localScale = scale;

            return transform;
        }

        public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
        }

        public static void DestroyAllChildren(this Transform transform)
        {
            foreach (var child in transform)
            {
                GameObject.Destroy(((Transform)child).gameObject);
            }
        }

        public static Transform GetRoot(this Transform transform)
        {
            var root = transform;
            while (root.parent != null)
            {
                root = root.parent;
            }

            return root;
        }

        public static Transform GetLastParentWithSameLayer(this Transform transform)
        {
            var result = transform;
            while (result.parent != null
                   && result.parent.gameObject.layer == transform.gameObject.layer)
            {
                result = result.parent;
            }

            return result;
        }

        public static Transform FindByTag(this Transform transform, string tag)
        {
            foreach (var child in transform)
            {
                var childTransform = (Transform)child;

                if (childTransform.tag == tag)
                {
                    return childTransform;
                }

                childTransform.FindByTag(tag);
            }

            return null;
        }

        public static Vector3 DirectionTo(this Transform from, Transform to)
        {
            return (to.position - from.position).normalized;
        }
    }
}