using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float LastTickAt;
    public bool IsStarted;
    public UnityEvent OnTenSeconds;

    public void StartTimer()
    {
        if (IsStarted)
        {
            return;
        }
        IsStarted = true;
        LastTickAt = Time.time;
    }

    public void StopTimer()
    {
        IsStarted = false;
        LastTickAt = Time.time;
    }

    void Update()
    {
        if (IsStarted && Time.time - LastTickAt >= 10)
        {
            LastTickAt = Time.time;
            OnTenSeconds.Invoke();
        }
    }
}
