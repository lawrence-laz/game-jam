using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEneter : MonoBehaviour {

    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public LayerMask Mask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Mask != (Mask | (1 << collision.gameObject.layer)))
        {
            return;
        }

        OnEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Mask != (Mask | (1 << collision.gameObject.layer)))
        {
            return;
        }

        OnExit.Invoke();
    }
}
