using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SteppedOnEvent : UnityEvent<GameObject[]> { }

public class TileObject : MonoBehaviour
{
    public Map Map { get; set; }
    public Death Death { get; set; }
    public Vector2 Position { get; set; }

    public SteppedOnEvent OnStepped;

    private void Start()
    {
        Map = FindObjectOfType<Map>();
        Position = transform.position;
    }

    // This is called manually/directly from turn manager to ensure it is called after all other objects have taken a turn.
    public void OnTurnEnded()
    {
        var tileObjects = Map.GetAll(Position).Where(x => x != gameObject).ToArray();
        if (tileObjects.Length != 0)
        {
            OnStepped.Invoke(tileObjects);
        }
    }
}
