using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] new AudioSource audio;

    public UnityEvent ItemGiven;

    private void OnTriggerStay(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null)
            return;

        GameObject clientObject = ClientsManager.Instance.ActiveClient;
        if (clientObject == null)
        {
            Debug.Log("Destroying " + item.Name);
            Destroy(item.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ForSaleBehaviour>() != null)
            return;

        Item item = other.GetComponent<Item>();
        if (item == null)
            return;

        GameObject clientObject = ClientsManager.Instance.ActiveClient;
        if (clientObject == null)
        {
            DisposeItem(item);
            return;
        }

        Debug.Log("Taking " + item.Name);
        clientObject
            .GetComponent<ClientBehaviour>()
            .Take(item);
        ItemGiven.Invoke();
    }

    private void DisposeItem(Item item)
    {
        Debug.Log("Disposing " + item.Name);
        Destroy(item.gameObject);
        audio.PlayOneShot(item.DropSound);
    }
}
