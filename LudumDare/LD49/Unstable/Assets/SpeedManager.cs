using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float Modifier = 0.8f;
    public float SpeedIncrease = 0.1f;
    public float IncreaseSpeedPeriod = 3f;
    public float LastIncreaseAt = 0;

    private void Update()
    {
        if (Time.timeSinceLevelLoad > LastIncreaseAt + IncreaseSpeedPeriod)
        {
            LastIncreaseAt = Time.timeSinceLevelLoad;
            Modifier += SpeedIncrease;
        }
    }
}
