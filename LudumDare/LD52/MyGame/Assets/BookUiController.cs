using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BookUiController : MonoBehaviour
{
    public AudioClip PageCloseSound;

    private bool _isBeingDestroyed = false;

    private void Start()
    {
        if (GameObject.FindObjectsOfType<BookUiController>().Count(book => !book._isBeingDestroyed) > 1)
        {
            _isBeingDestroyed = true;
            Destroy(gameObject);
        }
    }

    public void OpenBook()
    {
        Camera.main.transform.Find("Canvas/BookBackground").gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Q))
        {
            if (Camera.main.transform.Find("Canvas/BookBackground").gameObject.activeSelf)
            {
                GetComponent<AudioSource>().PlayOneShot(PageCloseSound);
            }
            Camera.main.transform.Find("Canvas/BookBackground").gameObject.SetActive(false);
        }
    }
}
