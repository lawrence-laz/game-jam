using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour
{
    public ParticleSystem ParticleSystem { get; private set; }

    private void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ParticleSystem.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
