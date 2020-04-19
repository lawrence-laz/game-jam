using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public Tilemap Tilemap { get; set; }

    private void Start()
    {
        Tilemap = FindObjectOfType<Tilemap>();
    }

    public GameObject Get(Vector2 position)
    {
        var targetPosition = position + new Vector2(0, -.1f);
        var targetCellPosition = Tilemap.WorldToCell(targetPosition);
        //var tile = Tilemap.GetTile(targetCellPosition);
        var tile = Tilemap.GetInstantiatedObject(targetCellPosition);

        if (tile != null) return tile;

        return FindObjectsOfType<TileObject>()
            .Where(x => x.Position == position)
            .FirstOrDefault()
            ?.gameObject;
    }

    public IEnumerable<GameObject> GetAll(Vector2 position)
    {
        var targetPosition = position + new Vector2(0, -.1f);
        var targetCellPosition = Tilemap.WorldToCell(targetPosition);
        //var tile = Tilemap.GetTile(targetCellPosition);
        var tile = Tilemap.GetInstantiatedObject(targetCellPosition);

        if (tile != null) yield return tile;

        foreach (var obj in FindObjectsOfType<TileObject>().Where(x => x.Position == position).Select(x => x.gameObject))
        {
            yield return obj;
        }
    }

    public bool IsTraversible(Vector2 position, GameObject source = null)
    {
        var gameObjects = GetAll(position);
        foreach (var obj in gameObjects)
        {
            if (obj.name.StartsWith("wall")) return false;
            if ((source?.tag == "Enemy" || source?.GetComponent<Box>() != null) && obj.GetComponent<Box>() != null) return false;
        }

        return true;
    }
}
