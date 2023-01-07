using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookUiController : MonoBehaviour
{
    public void OpenBook()
    {
        Camera.main.transform.Find("Canvas/BookBackground").gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Q))
        {
            Camera.main.transform.Find("Canvas/BookBackground").gameObject.SetActive(false);
        }
    }
}
