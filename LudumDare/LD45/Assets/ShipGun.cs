using UnityEngine;

public class ShipGun : MonoBehaviour
{
    public float FireFrequency;
    public bool IsShooting;
    public GameObject Bullet;

    private float lastShotAt;

    public AudioSource AudioSource { get; private set; }

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        IsShooting = Input.GetMouseButton(0);
        if (IsShooting && Time.time - lastShotAt >= FireFrequency)
        {
            lastShotAt = Time.time;
            var bullet = Instantiate(Bullet);
            if (AudioSource != null)
                AudioSource.Play();
            bullet.transform.position = transform.position;
            bullet.transform.LookAt(transform.position + transform.forward * 100);
            //bullet.transform.localRotation = bullet.transform.localRotation * Quaternion.AngleAxis(Random.Range(0, 360), bullet.transform.forward);
        }
    }
}
