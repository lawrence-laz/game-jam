using Libs.Base.Extensions;
using UnityEngine;

namespace Libs.Base.GameLogic.AudioSource
{
    public class PlayRandomAudioClipBehaviour : MonoBehaviour
    {
        [SerializeField] UnityEngine.AudioSource audioSource;
        [SerializeField] AudioClip[] audioClips;

        [ContextMenu("PlayRandomAudioClip")]
        public void PlayRandomAudioClip()
        {
            if (audioSource == null)
                return;

            audioSource.PlayOneShot(audioClips.GetRandom());
        }
    }
}
