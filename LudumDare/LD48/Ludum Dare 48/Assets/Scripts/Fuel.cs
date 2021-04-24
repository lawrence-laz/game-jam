using UnityEngine;

public class Fuel : Value
{
    public float MaxValue;
    public float CurrentValue;
    public override float NormalizedValue => CurrentValue / MaxValue;

    private void Update()
    {
        CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);
    }
}
