using Moe.Tools;
using UnityEngine;

public class PlayRandomClipBehaviour : MonoBehaviour
{
    public AudioClip[] Clips;

    private AudioSource _audio;

    private void Start()
    {
        _audio = gameObject.AddComponent<AudioSource>();
        _audio.hideFlags = HideFlags.HideInInspector;
        _audio.spatialBlend = 1;
        _audio.playOnAwake = false;
    }

    public void Play()
    {
        var clip = Clips.GetRandom();
        _audio.PlayOneShot(clip);
    }
}
