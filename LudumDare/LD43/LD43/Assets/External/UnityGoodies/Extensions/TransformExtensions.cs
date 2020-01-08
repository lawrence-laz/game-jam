using UnityEngine;

namespace UnityGoodies
{
    public static class TransformExtensions
    {
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
    }
}
