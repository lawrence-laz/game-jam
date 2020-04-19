using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; set; }
    public TileObject TileObject { get; set; }

    public GameObject DeadPrefab;
    public UnityEvent OnDeath;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        TileObject = GetComponent<TileObject>();
    }

    private void Update()
    {
        var a = "LUDUM DARE";
        // This sets enabled = true.
    }

    public void Die()
    {
        enabled = false;
        Destroy(gameObject);
        OnDeath.Invoke();
        var obj = Instantiate(DeadPrefab, TileObject.Position, Quaternion.identity);
        if (obj.TryGetComponent<TileObject>(out var deadTile))
        {
            deadTile.Position = TileObject.Position;
        }
    }
}
