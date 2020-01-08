using UnityEngine;
using UnityEngine.Events;

public class InteractableBehaviour : MonoBehaviour
{
    [SerializeField] float distance = 3;

    public UnityEvent OnInteract;

    public virtual bool Interact(Interactor interactor)
    {
        if (!enabled)
            return false;

        if ((interactor.transform.position - transform.position).magnitude > distance)
            return false;

        OnInteract?.Invoke();
        return false;
    }

    [ContextMenu("Set distance to current")]
    private void SetDistanceToCurrent()
    {
        distance = (GameObject.FindWithTag("Player").transform.position - transform.position).magnitude;
    }

    private void Update()
    {

    }
}
