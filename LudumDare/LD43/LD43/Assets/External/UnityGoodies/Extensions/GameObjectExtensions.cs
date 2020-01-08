using System;
using UnityEngine;

namespace UnityGoodies
{
    public static class GameObjectExtensions
    {
        public static Component GetComponentInChildrenOrParent(this GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponentInChildren(type);
            if (!component)
            {
                component = gameObject.GetComponentInParent(type);
            }

            return component;
        }

        public static T GetComponentInChildrenOrParent<T>(this GameObject gameObject) where T : Component
        {
            return (T)gameObject.GetComponentInChildrenOrParent(typeof(T));
        }

        public static Component AddComponentIfNotExists(this GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponent(type);
            if (component == null)
            {
                component = gameObject.AddComponent(type);
            }

            return component;
        }

        public static T AddComponentIfNotExists<T>(this GameObject gameObject) where T : Component
        {
            return (T)gameObject.AddComponentIfNotExists(typeof(T));
        }

        public static void RemoveComponent<T1>(this GameObject gameObject) where T1 : Component
        {
            var comp1 = gameObject.GetComponent<T1>();
            if (comp1 != null)
            {
                UnityEngine.Object.Destroy(comp1);
            }
        }

        public static void RemoveComponents<T1>(this GameObject gameObject) where T1 : Component
        {
            var components = gameObject.GetComponents<T1>();
            foreach (var component in components)
            {
                UnityEngine.Object.Destroy(component);
            }
        }

        public static void EnableComponentsInChildren(this GameObject gameObject, Type type, bool enable)
        {
            foreach (var component in gameObject.GetComponentsInChildren(type))
            {
                ((Behaviour)component).enabled = enable;
            }
        }

        public static void EnableCollidersInChildren(this GameObject gameObject, bool enable = true)
        {
            foreach (var component in gameObject.GetComponentsInChildren(typeof(Collider)))
            {
                ((Collider)component).enabled = enable;
            }
        }

        public static void EnableComponentsInChildren<T>(this GameObject gameObject, bool enable) where T : Behaviour
        {
            gameObject.EnableComponentsInChildren(typeof(T), enable);
        }

        public static void EnableComponentsInChildren(this GameObject gameObject, bool enable, params Type[] types)
        {
            foreach (var type in types)
            {
                gameObject.EnableComponentsInChildren(type, enable);
            }
        }

        public static void EnableComponents(this GameObject gameObject, Type type, bool enable)
        {
            foreach (var component in gameObject.GetComponents(type))
            {
                ((Behaviour)component).enabled = enable;
            }
        }

        public static void EnableComponents<T>(this GameObject gameObject, bool enable = true) where T : Behaviour
        {
            gameObject.EnableComponents(typeof(T), enable);
        }

        public static void EnableComponents(this GameObject gameObject, bool enable, params Type[] types)
        {
            foreach (var type in types)
            {
                gameObject.EnableComponents(type, enable);
            }
        }

        public static bool IsInLayerFrom(this GameObject gameObject, LayerMask layerMask)
        {
            return (1 << gameObject.layer | layerMask) == layerMask;
        }
    }
}
