using UnityEngine;
using UnityEngine.Events;
using UnityGoodies;

public class CollisionEventBehaviour : MonoBehaviour
{
    public LayerMask Mask;

    public UnityEvent OnCollisionEntered;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.GetRoot().gameObject.IsInLayerFrom(Mask))
        {
            Debug.LogFormat("{0} entered hit collider {1}, but no event because doesn't match mask", collision.gameObject.name, gameObject.name);
            return;
        }

        OnCollisionEntered.Invoke();
    }
}
