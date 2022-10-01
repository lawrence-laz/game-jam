using UnityEngine;
using UnityEngine.Events;

public class OnDestroyEmitter : MonoBehaviour
{
    public UnityEvent BeforeDestroy;

    private void OnDestroy()
    {
        BeforeDestroy.Invoke();
    }
}
