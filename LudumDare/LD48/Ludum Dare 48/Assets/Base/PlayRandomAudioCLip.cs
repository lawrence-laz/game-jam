using UnityEngine;

public class PlayRandomAudioCLip : MonoBehaviour
{
    public AudioClip[] RandomAudioClip;
    public Vector2 RandomPitch;

    private AudioSource _audio;
    private float _startingPitch;

    private void OnEnable()
    {
        _audio = GetComponent<AudioSource>();
        _startingPitch = _audio.pitch;
    }

    public void Play()
    {
        if (RandomAudioClip != null && RandomAudioClip.Length > 0)
        {
            var clip = RandomAudioClip[Random.Range(0, RandomAudioClip.Length)];
            _audio.clip = clip;
            _audio.pitch = _startingPitch + Random.Range(RandomPitch.x, RandomPitch.y);
        }

        _audio.Play();
    }
}
