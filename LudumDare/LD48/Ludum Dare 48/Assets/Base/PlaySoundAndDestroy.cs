using UnityEngine;
using UnityEngine.Events;

public class PlaySoundAndDestroy : MonoBehaviour
{
    public Vector2 PitchRandomization;
    public Vector2 AbsolutePitchRandom;
    public bool UseAbsolutePitch;
    public AudioClip[] RandomAudioClip;
    public bool PlayOnAwake;
    public UnityEvent BeforeDestroy;

    private bool _wasPlayed = false;
    private AudioSource _audio;

    private void Start()
    {
        if (PlayOnAwake)
            Play();
    }

    [ContextMenu("Play")]
    public void Play()
    {
        transform.SetParent(null);
        _audio = GetComponent<AudioSource>();
        if (RandomAudioClip != null && RandomAudioClip.Length > 0)
        {
            var clip = RandomAudioClip[Random.Range(0, RandomAudioClip.Length)];
            _audio.clip = clip;
        }

        if (UseAbsolutePitch)
        {
            var randomChange = Random.Range(AbsolutePitchRandom.x, AbsolutePitchRandom.y);
            _audio.pitch = randomChange;
        }
        else
        {
            var randomChange = Random.Range(PitchRandomization.x, PitchRandomization.y);
            _audio.pitch += randomChange;
        }


        _audio.Play();
        _wasPlayed = true;
    }

    private void Update()
    {
        if (_wasPlayed && !_audio.isPlaying)
        {
            Destroy(gameObject);
            BeforeDestroy.Invoke();
            Debug.Log("Invoked before destroy");
        }
    }
}
