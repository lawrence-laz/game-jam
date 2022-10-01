using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
    }
}
