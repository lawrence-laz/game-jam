using System.Linq;
using UnityEngine;

public class Music : MonoBehaviour
{
    private void Start()
    {
        if (FindObjectsOfType<Music>().Any(x => x != this))
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
