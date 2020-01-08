using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardEvent : MonoBehaviour
{
    public UnityEvent OnEscapePressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapePressed.Invoke();
        }
    }
}
