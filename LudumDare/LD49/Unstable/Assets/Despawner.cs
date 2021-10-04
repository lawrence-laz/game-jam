using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Camera>(out var _) 
            || other.TryGetComponent<PlayerController>(out var _)
            || ((1 << other.gameObject.layer) & LayerMask.GetMask("Player")) != 0)
            return;

        Debug.Log("Destroying", other.gameObject);
        Destroy(other.gameObject);
    }
}
