using UnityEngine;

public class JukeBoxBehaviour : MonoBehaviour
{
    private void Start()
    {
        if (FindObjectsOfType<JukeBoxBehaviour>().Length > 1)
            DestroyImmediate(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}
