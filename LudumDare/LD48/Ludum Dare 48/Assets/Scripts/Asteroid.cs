using UnityEngine;

public class Asteroid : Value
{
    public float MaxValue;
    public float CurrentValue;
    public override float NormalizedValue => CurrentValue / MaxValue;
    public float SpeedMultiplier = 1;

    private void Update()
    {
        CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);
    }
}
