using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Map Map { get; set; }

    private void Start()
    {
        Map = FindObjectOfType<Map>();

        foreach (var affected in Map.Get(transform.position).GetComponents<Death>())
        {
            affected.Die();
        }
    }
}
