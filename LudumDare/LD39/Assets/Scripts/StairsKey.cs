using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsKey : MonoBehaviour
{
    [SerializeField]
    Portal[] otherDoors;
    [SerializeField]
    Portal stairsDoors;
    [SerializeField]
    Transform stairsDoorsTarget;

    public void OpenStairsDoor()
    {
        stairsDoors.targetPosition = stairsDoorsTarget;
        //foreach (Portal doors in otherDoors)
        //{
        //    doors.lockedMessage = new string[] { "Key doesn't fit... I should keep looking." };
        //}
        GetComponent<TextTrigger>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        enabled = false;
    }
}
