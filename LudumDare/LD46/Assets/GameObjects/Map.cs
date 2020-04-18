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
            .Where(x => (Vector2)x.transform.position == position)
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

        foreach (var gameObject in FindObjectsOfType<TileObject>().Where(x => (Vector2)x.transform.position == position).Select(x => x.gameObject))
        {
            yield return gameObject;
        }
    }

    public bool IsTraversible(Vector2 position)
    {
        var gameObject = Get(position + new Vector2(0, -.1f));
        return gameObject == null || !gameObject.name.StartsWith("wall");
    }
}
