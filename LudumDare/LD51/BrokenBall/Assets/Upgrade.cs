using System.Diagnostics;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Vector2 Velocity;
    

    void Update()
    {
        transform.position += (Vector3)Velocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GlobalAudio.Instance.Play(GlobalAudio.Instance.Upgrade);
        Destroy(gameObject);
    }
}
