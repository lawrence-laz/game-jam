using UnityEngine;

public class SoundMaster : MonoBehaviour
{
    public AudioClip AngryBoss;
    public AudioClip DrawCard;
    public AudioClip Exercise;
    public AudioClip Explosion;
    public AudioClip GameOver;
    public AudioClip Gaming;
    public AudioClip Keyboard;
    public AudioClip LightSwitch;
    public AudioClip NightAmbience;
    public AudioClip Snoring;
    public AudioClip Eating;
    public AudioClip Drink;

    public AudioSource AudioSource;
    public AudioSource AudioSourceOneshot;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSourceOneshot = gameObject.AddComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    private void Update()
    {
        AudioSource.pitch = Time.timeScale;
    }

    internal void PlayOnce(AudioClip clip)
    {
        AudioSourceOneshot.PlayOneShot(clip);
    }
}
