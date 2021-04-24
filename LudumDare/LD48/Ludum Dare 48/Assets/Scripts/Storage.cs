using UnityEngine;

public class Storage : Value
{
    public float MaxValue;
    public float CurrentValue;
    public override float NormalizedValue => CurrentValue / MaxValue;
    public bool HasSpace => CurrentValue < MaxValue;

    private void Update()
    {
        CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);
    }
}
