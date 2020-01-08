using Libs.Base.Extensions;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float FireRate = 2;
    public float FireDistance = 15;
    public GameObject Bullet;

    private float lastShotAt = 0;

    public GameObject Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Player.transform.DistanceTo(transform) > FireDistance)
            return;

        if (lastShotAt + FireRate <= Time.time)
        {
            lastShotAt = Time.time;
            var bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            bullet.transform.LookAt(Player.transform);
        }
    }
}
