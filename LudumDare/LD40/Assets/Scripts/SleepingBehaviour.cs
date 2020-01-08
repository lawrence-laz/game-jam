using UnityEngine;

public class SleepingBehaviour : MonoBehaviour
{
    private GameObject zzzEffect;
    private float sleepUntill = 0;

    public GameObject ZzzEffect { get { return zzzEffect; } set { zzzEffect = value; } }

    public void SleepFor(float seconds)
    {
        sleepUntill = Time.time + seconds;
    }

    private void OnDestroy()
    {
        Destroy(zzzEffect);
    }

    private void Update()
    {
        CheckIfShouldWakeUp();
    }

    private void CheckIfShouldWakeUp()
    {
        if (Time.time >= sleepUntill)
            Destroy(this);
    }
}
