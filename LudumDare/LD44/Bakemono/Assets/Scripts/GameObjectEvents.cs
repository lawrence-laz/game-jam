using UnityEngine;
using UnityEngine.Events;

public class GameObjectEvents : MonoBehaviour
{
    public UnityEvent OnEnalbeEvent;
    public UnityEvent OnStart;
    public UnityEvent OnDisableEvent;

    private void OnDisable()
    {
        OnDisableEvent.Invoke();
    }

    private void Start()
    {
        OnStart.Invoke();
    }

    private void OnEnable()
    {
        OnEnalbeEvent.Invoke();
    }
}
