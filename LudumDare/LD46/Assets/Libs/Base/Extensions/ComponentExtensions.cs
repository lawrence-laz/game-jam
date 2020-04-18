using System;
using UnityEngine;

namespace Libs.Base.Extensions
{
    public static class ComponentExtensions
    {
        public static void ForAllComponents<T>(this Component current, Action<T> action) where T : Component
        {
            foreach (var component in current.GetComponents<T>())
            {
                action.Invoke(component);
            }
        }

        public static bool ForAllComponentsInChildren<T>(this Component current, Action<T> action) where T : Component
        {
            var components = current.GetComponentsInChildren<T>();

            if (components.Length == 0)
            {
                return false;
            }

            foreach (var component in components)
            {
                action.Invoke(component);
            }

            return true;
        }

        public static bool ForAllComponentsInRootsChildren<T>(this Component current, Action<T> action) where T : Component
        {
            return current.transform.GetRoot().ForAllComponentsInChildren(action);
        }
    }
}
