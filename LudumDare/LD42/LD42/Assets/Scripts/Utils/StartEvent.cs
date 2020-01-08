using UnityEngine;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour
{
    public UnityEvent OnStart;

    private void Start()
    {
        if (OnStart != null)
            OnStart.Invoke();
    }
}
