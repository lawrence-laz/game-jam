using UnityEngine;

public class AudioPlayerBehaviour : MonoBehaviour
{
    public AudioClip _sound;

    private AudioSource _audio;

    private void Start()
    {
        _audio = gameObject.AddComponent<AudioSource>();
        _audio.clip = _sound;
        _audio.loop = true;
        _audio.Play();
    }
}
