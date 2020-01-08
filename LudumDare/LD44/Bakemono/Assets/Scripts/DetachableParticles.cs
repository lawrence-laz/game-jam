using UnityEngine;

public class DetachableParticles : MonoBehaviour
{
    private ParticleSystem _particles;

    private void OnEnable()
    {
        transform.SetParent(null);
        _particles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!_particles.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
