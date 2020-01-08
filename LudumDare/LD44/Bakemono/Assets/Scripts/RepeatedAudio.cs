using UnityEngine;

public class RepeatedAudio : MonoBehaviour
{
    public int PlayTimes;
    public float MinPeriodToReset = 0.02f;

    private float _lastPlay = 0;
    private AudioSource _audio;

    public static RepeatedAudio Get(string name)
    {
        return GameObject.Find(name).GetComponent<RepeatedAudio>();
    }

    private void OnEnable()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PlayTimes <= 0)
            return;

        if (_audio.isPlaying && Time.time - _lastPlay < MinPeriodToReset)
            return;

        if (_audio.isPlaying)
            _audio.Stop();

        PlayTimes--;
        _lastPlay = Time.time;
        _audio.Play();
    }
}
