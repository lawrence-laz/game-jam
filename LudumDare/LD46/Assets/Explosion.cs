using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Map Map { get; set; }
    public TileObject TileObject { get; set; }

    private void Start()
    {
        Map = FindObjectOfType<Map>();
        TileObject = GetComponent<TileObject>();

        var objects = Map.GetAll(transform.position).ToArray();

        foreach (var affected in objects)
        {
            if (affected.TryGetComponent<Death>(out var death) && death.enabled)
            {
                death.Die();
            }
        }
    }
}
