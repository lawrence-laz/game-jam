using System;
using UnityEngine;

public class GroundTriggerComponent : MonoBehaviour
{
    public event Action<HealthComponent> OnOutOfBounds;
    public event Action<Transform> OnOutOfBoundsItem;

    private void Start()
    {
        OnOutOfBounds += OutOfBoundsSystem.Instance.HandleOutOfBounds;
        OnOutOfBoundsItem += OutOfBoundsSystem.Instance.HandleOutOfBoundsItem;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            if (OnOutOfBounds != null)
                OnOutOfBounds.Invoke(other.GetComponent<HealthComponent>());
        }
        else if (other.transform.parent.parent == null)
        {
            if (OnOutOfBoundsItem != null)
                OnOutOfBoundsItem.Invoke(other.transform);
        }
    }
}
