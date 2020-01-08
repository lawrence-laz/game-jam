using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{
    public UnityEvent OnStartEvent;

    private void Start()
    {
        OnStartEvent.Invoke();
    }

}
