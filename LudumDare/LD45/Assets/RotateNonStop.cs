using UnityEngine;

public class RotateNonStop : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.Rotate(Vector3.forward, Speed * Time.deltaTime);
    }
}
