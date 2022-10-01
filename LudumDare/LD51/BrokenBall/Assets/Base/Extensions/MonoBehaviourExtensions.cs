using System.Collections;
using UnityEngine;

namespace Libs.Base.Extensions
{
    public static class MonoBehaviourExtensions
    {
        // TODO, fix, Param should be Func<IEnumerator>
        public static Coroutine StartOrRestartCoroutine(this MonoBehaviour behaviour, ref Coroutine saved,
            IEnumerator started)
        {
            if (saved != null) behaviour.StopCoroutine(started);

            saved = behaviour.StartCoroutine(started);

            return saved;
        }

        public static IEnumerator StartOrRestartCoroutine(this MonoBehaviour behaviour, ref IEnumerator saved,
            IEnumerator started)
        {
            if (saved != null) behaviour.StopCoroutine(started);

            saved = started;
            behaviour.StartCoroutine(started);

            return saved;
        }

        // TODO, fix, Param should be Func<IEnumerator>
        public static Coroutine StartCoroutineIfNotStarted(this MonoBehaviour behaviour, ref Coroutine saved,
            IEnumerator started)
        {
            if (saved != null)
                return saved;

            saved = behaviour.StartCoroutine(started);

            return saved;
        }

        public static IEnumerator StartCoroutineIfNotStarted(this MonoBehaviour behaviour, ref IEnumerator saved,
            IEnumerator started)
        {
            if (saved != null)
                return saved;

            saved = started;
            behaviour.StartCoroutine(started);

            return saved;
        }

        public static void StopCoroutineIfRunning(this MonoBehaviour behaviour, Coroutine coroutine)
        {
            if (coroutine == null)
                return;

            behaviour.StopCoroutine(coroutine);
        }
    }
}