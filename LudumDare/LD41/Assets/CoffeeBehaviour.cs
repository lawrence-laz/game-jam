using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoffeeBehaviour : MonoBehaviour
{
    public float heightToActivate;
    public UnityEvent Activated;
    public UnityEvent Deactivated;
    bool isActivated = false;

    private void Update()
    {
        if (!isActivated && transform.position.y >= heightToActivate)
        {
            isActivated = true;
            Activated.Invoke();
        }
        else if (isActivated && transform.position.y < heightToActivate)
        {
            isActivated = false;
            Deactivated.Invoke();
        }
    }

}
