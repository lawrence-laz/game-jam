using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Destroyer destroying: {other.gameObject}");
        Destroy(other.gameObject);
    }
}
