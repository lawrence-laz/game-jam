using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    public GameObject SpaceStationUI;

    // Called by message.
    public void OnAttach(GameObject who)
    {
        if (who.CompareTag("Player"))
        {
            SpaceStationUI.SetActive(true);
        }
    }

    // Called by message.
    public void OnDetach(GameObject who)
    {
        if (who.CompareTag("Player"))
        {
            SpaceStationUI.SetActive(false);
        }
    }
}
