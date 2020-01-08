using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] float reachDistance = 4f;
    [SerializeField] LayerMask mask;

    private new AudioSource audio;

    public UnityEventGeneric<InteractableBehaviour> OnInteracted = new UnityEventGeneric<InteractableBehaviour>();

    public void CastInteractionRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, reachDistance, mask.value))
            return;

        PlayItemSound(hit.collider.gameObject);

        InteractableBehaviour[] inChildren = hit.collider.GetComponents<InteractableBehaviour>();
        List<InteractableBehaviour> inParents = hit.collider.GetComponentsInParent<InteractableBehaviour>().Distinct().ToList();

        foreach (InteractableBehaviour interactable in inChildren)
        {
            if (inParents.Contains(interactable))
                continue;

            if (interactable.Interact(this))
            {
                OnInteracted.Invoke(interactable);
                return;
            }
        }
        foreach (InteractableBehaviour interactable in inParents)
        {
            if (interactable.Interact(this))
            {
                OnInteracted.Invoke(interactable);
                return;
            }
        }
    }

    private void PlayItemSound(GameObject itemObject)
    {
        Item item = itemObject.GetComponent<Item>();
        if (item == null)
            return;
        audio.PlayOneShot(item.PickUpSound);
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CastInteractionRay();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * reachDistance);
    }
}
