using System.Collections;
using Libs.Base.Extensions;
using UnityEngine;

namespace Libs.Base.GameLogic.AudioSource
{
    public class AudioSourceEffectsBehaviour : MonoBehaviour
    {
        [SerializeField] UnityEngine.AudioSource audioSource = null;

        private Coroutine fadeCoroutine;

        public void FadeOut(float duration)
        {
            //Debug.Log("FadeOut");
            this.StartOrRestartCoroutine(ref fadeCoroutine, FadeCoroutine(0, duration));
        }

        public void FadeIn(float duration)
        {
            //Debug.Log("FadeIn");
            this.StartOrRestartCoroutine(ref fadeCoroutine, FadeCoroutine(1, duration));
        }

        private IEnumerator FadeCoroutine(float target, float duration)
        {
            while (audioSource.volume != target)
            {
                audioSource.volume = Mathf.MoveTowards(audioSource.volume, target, 1f / (15 * duration));

                yield return new WaitForSeconds(1f / 15);
            }
        }
    }
}
