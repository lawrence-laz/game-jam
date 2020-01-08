using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TextTrigger : MonoBehaviour
{
    [SerializeField]
    public string[] messages = new string[] { };
    [SerializeField]
    float typeDelay = 0.05f;
    [SerializeField]
    public GameObject sender;
    [SerializeField]
    bool requireAction = true;
    [SerializeField]
    public bool onlyOnce = false;

    new AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!enabled)
            return;

        if ((!requireAction || Action.Instance.action) && TextManager.Instance.AllowMovement && other.gameObject.tag == "Player")
        {
            Action.Instance.action = false;
            if (audio)
                audio.Play();

            TextManager.Instance.ShowTextTyped(messages, typeDelay, sender ?? gameObject);
            if (onlyOnce)
                enabled = false;
        }
    }
}
