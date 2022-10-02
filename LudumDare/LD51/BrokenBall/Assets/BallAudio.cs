using UnityEngine;

public class BallAudio : MonoBehaviour
{
    public AudioClip OnPaddleHit;
    public AudioClip OnBoundaryHit;
    public AudioClip OnTileHit;

    private AudioSource _audio;

    private void Start() {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayPaddleHit() => _audio.PlayOneShot(OnPaddleHit);
    public void PlayBoundaryHit() => _audio.PlayOneShot(OnBoundaryHit);
    public void PlayTileHit() => _audio.PlayOneShot(OnTileHit);
}
