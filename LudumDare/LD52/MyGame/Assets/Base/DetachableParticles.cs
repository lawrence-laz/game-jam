using UnityEngine;

public class DetachableParticles : MonoBehaviour
{
    private ParticleSystem _particles;

    public void Play()
    {
        gameObject.SetActive(true);
        if (transform.parent != null)
        {
            transform.SetParent(null, true);
            _particles = GetComponent<ParticleSystem>();
        }

        _particles.Play();
    }

    private void Update()
    {
        if (transform.parent != null)
        {
            transform.SetParent(null, true);
            _particles = GetComponent<ParticleSystem>();
        }

        if (!_particles.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
