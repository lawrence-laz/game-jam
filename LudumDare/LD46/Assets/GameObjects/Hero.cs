using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public List<Hostage> Hostages;

    public TurnManager TurnManager { get; set; }
    public TileObject TileObject { get; set; }
    public Map Map { get; set; }

    private void Start()
    {
        TurnManager = FindObjectOfType<TurnManager>();
        Map = FindObjectOfType<Map>();
        TurnManager.OnTurnEnded.AddListener(OnTurnEnded);
        TileObject = GetComponent<TileObject>();
    }

    private void OnTurnEnded()
    {
        foreach (var position in TileObject.Position.Around())
        {
            var targetGameObject = Map.Get(position);
            if (targetGameObject.TryGetComponentSafe<Hostage>(out var hostage) && !Hostages.Contains(hostage))
            {
                hostage.Follow(Hostages.Count == 0 ? TileObject : Hostages.Last().GetComponent<TileObject>());
                Hostages.Add(hostage);
            }
        }
    }
}
