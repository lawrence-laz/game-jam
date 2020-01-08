using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ImpactEffect;
    public float BulletSpeed = 80;

    public Rigidbody Rigidbody { get; private set; }
    public AudioSource AudioSource { get; private set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, 10);
        AudioSource = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        Rigidbody.MovePosition(transform.position + transform.forward * BulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ImpactEffect, transform.position, Quaternion.identity);

        if (AudioSource != null)
        {
            AudioSource.Play();
            AudioSource.transform.SetParent(null);
            Destroy(AudioSource.gameObject, 0.5f);
            Destroy(gameObject);
        }
    }
}
