using System;
using UnityEngine;

namespace Libs.Base.Extensions
{
    public static class ActionExtensions
    {
        public static void SafeInvoke(this Action action)
        {
            if (action == null)
            {
                return;
            }

            foreach (var handler in action.GetInvocationList())
            {
                if (handler == null)
                {
#if UNITY_EDITOR
                    Debug.LogWarning("There are null handlers in some event.");
#endif
                    continue;
                }

                ((Action)handler).Invoke();
            }
        }

        public static void SafeInvoke<T>(this Action<T> action, T param)
        {
            if (action == null)
            {
                return;
            }

            foreach (var handler in action.GetInvocationList())
            {
                if (handler == null)
                {
#if UNITY_EDITOR
                    Debug.LogWarning("There are null handlers in some event.");
#endif
                    continue;
                }

                ((Action<T>)handler).Invoke(param);
            }
        }

        public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2)
        {
            if (action == null)
            {
                return;
            }

            foreach (var handler in action.GetInvocationList())
            {
                if (handler == null)
                {
#if UNITY_EDITOR
                    Debug.LogWarning("There are null handlers in some event.");
#endif
                    continue;
                }

                ((Action<T1, T2>)handler).Invoke(param1, param2);
            }
        }
    }
}
