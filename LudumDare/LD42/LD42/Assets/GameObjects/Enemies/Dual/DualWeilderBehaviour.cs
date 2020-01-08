using UnityEngine;

public class DualWeilderBehaviour : MonoBehaviour
{
    public float Speed;

    private HealthComponent _health;

    private void Start()
    {
        _health = GetComponentInParent<HealthComponent>();
    }

    private void Update()
    {
        if (_health.Health == 0)
            return;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z += Speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
