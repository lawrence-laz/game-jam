using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    Light redLight;
    [SerializeField]
    Light greenLight;
    [SerializeField]
    AudioClip turnOnSound;
    [SerializeField]
    AudioClip generatorSound;
    [SerializeField]
    GameObject[] otherSetActive;

    void TurnOn()
    {
        greenLight.enabled = true;
        redLight.enabled = false;

        GameObject[] sockets = GameObject.FindGameObjectsWithTag("Socket");
        foreach(GameObject socketObject in sockets)
        {
            Socket socket = socketObject.GetComponent<Socket>();
            if (socket == null)
                continue;

            socket.enabled = true;
            TextTrigger textTrigger = socket.GetComponent<TextTrigger>();
            textTrigger.messages = new string[] { "message:PlayPlug", "........", ".........", "message:TurnOn", "IT WORKS!!!", "message:Call911", "......." };
            textTrigger.sender = GameObject.FindWithTag("Phone");
            textTrigger.onlyOnce = true;
        }

        foreach (GameObject obj in otherSetActive)
        {
            obj.SetActive(true);
        }

        otherSetActive = GameObject.FindGameObjectsWithTag("Lamp");
        foreach (GameObject obj in otherSetActive)
        {
            foreach (Light light in obj.GetComponentsInChildren<Light>())
            {
                light.enabled = true;
            }
        }

        GameObject.FindWithTag("Whispers").GetComponent<AudioSource>().Play();

        GetComponent<AudioSource>().PlayOneShot(turnOnSound);
        Invoke("TurnOnGeneratorSound", 1);
    }

    void TurnOnGeneratorSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.loop = true;
        audio.clip = generatorSound;
        audio.Play();
    }
}
