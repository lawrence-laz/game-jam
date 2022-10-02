using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudio : Singleton<GlobalAudio>
{
    public AudioClip GameOver;
    public AudioClip Upgrade;

    private AudioSource _audio;

    private void Start() {
        _audio = GetComponent<AudioSource>();
    }
    
    public void Play(AudioClip clip) => _audio.PlayOneShot(clip);
}
