using UnityEngine;

public class DestroyBehindCamera : MonoBehaviour
{
    private void Update()
    {
        var direction = Camera.main.transform.DirectionTo(transform).normalized;
        direction.y = 0;
        var forward = Camera.main.transform.forward;
        forward.y = 0;
        float dotProduct = Vector3.Dot(direction.normalized, forward.normalized);
        if (dotProduct < -0.5f)
        {
            Debug.Log("Self destruct behind camera.");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(dotProduct);
        }
    }

    private void OnDestroy()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<ParticleSystem>(out var _))
            {
                child.SetParent(null, true);
            }
        }
    }
}
