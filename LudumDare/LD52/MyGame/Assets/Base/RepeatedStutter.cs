using System.Threading;
using UnityEngine;

public class RepeatedStutter : MonoBehaviour
{
    public static void DoIt()
    {
        if (Time.time - _lastPlay < MinPeriodToReset)
        {
            return;
        }

        _lastPlay = Time.time;
        Thread.Sleep(25);
    }
    public static float MinPeriodToReset = 0.1f;
    private static float _lastPlay = 0;
}
