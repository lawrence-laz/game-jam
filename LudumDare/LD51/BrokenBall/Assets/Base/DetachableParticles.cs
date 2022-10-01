using UnityEngine;

public class DetachableParticles : MonoBehaviour
{
    private ParticleSystem _particles;

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
