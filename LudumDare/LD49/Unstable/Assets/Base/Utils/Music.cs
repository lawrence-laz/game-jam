using UnityEngine;

public class Music : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
