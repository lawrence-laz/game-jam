using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ShotBy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
