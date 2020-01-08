using UnityEngine;

public class ObjectSpawnerBehaviour : MonoBehaviour
{
    public void Create(GameObject prefab)
    {
        Instantiate(prefab);
    }
}
