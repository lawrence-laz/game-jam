using UnityEngine;
using UnityEngine.AI;

public class NavAgentWalkSound : MonoBehaviour {
    public AudioClip Sound;

    private NavMeshAgent _nav;
    private AudioSource _audio;

    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();

        _audio = gameObject.AddComponent<AudioSource>();
        _audio.playOnAwake = false;
        _audio.clip = Sound;
        _audio.spatialBlend = 1;
        _audio.loop = true;
    }

    private void Update()
    {
        if (_audio.isPlaying && _nav.velocity.sqrMagnitude < 0.05f)
        {
            _audio.Stop();
        }
        else if (!_audio.isPlaying && _nav.velocity.sqrMagnitude > 0.05f)
        {
            _audio.Play();
        }
    }
}
