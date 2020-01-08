using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BennetsKey : MonoBehaviour
{
    [SerializeField]
    Portal[] otherDoors;
    [SerializeField]
    Portal bennetsDoors;
    [SerializeField]
    Transform bennetsDoorsTarget;

    public void OpenBennetsDoor()
    {
        bennetsDoors.targetPosition = bennetsDoorsTarget;
        foreach (Portal doors in otherDoors)
        {
            doors.lockedMessage = new string[] { "Key doesn't fit... I should keep looking." };
        }
        GetComponent<TextTrigger>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        enabled = false;
    }
}
