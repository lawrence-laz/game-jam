using UnityEngine;

public class Asteroid : Value
{
    public float MaxValue;
    public float CurrentValue;
    public override float NormalizedValue => CurrentValue / MaxValue;
    public float SpeedMultiplier = 1;
    public GameObject ShatterFX;
    public bool BigExposion;

    private void Start()
    {
        transform.Rotate(Vector3.forward, Random.Range(0, 360));
    }

    private void Update()
    {
        CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);

        if (CurrentValue == 0)
        {
            var fx =Instantiate(ShatterFX, transform.position, transform.rotation);
            if (BigExposion)
            {
                fx.GetComponent<ShatterFX>().ExplosionForce *= 10;
            }
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<Collider2D>());
            enabled = false;
        }
    }
}
