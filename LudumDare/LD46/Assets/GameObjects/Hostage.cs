using UnityEngine;

public class Hostage : MonoBehaviour
{
    public TurnManager TurnManager { get; set; }
    public Move Move { get; set; }
    public TileObject TargetToFollow { get; set; }
    public Vector2 NextPosition { get; set; }
    public Map Map { get; set; }

    private void Start()
    {
        Move = GetComponent<Move>();
        TurnManager = FindObjectOfType<TurnManager>();
        TurnManager.OnTurnStarted.AddListener(OnTurnStarted);
        TurnManager.OnTurnEnded.AddListener(OnTurnEnded);
        Map = FindObjectOfType<Map>();
    }

    private void OnTurnStarted()
    {
        if (TargetToFollow == null) return;

        if (Map.Get(NextPosition)?.tag == "Player") return;

        Move.MoveTo(NextPosition);
    }

    private void OnTurnEnded()
    {
        if (TargetToFollow == null) return;

        NextPosition = TargetToFollow.Position;
    }

    public void Follow(TileObject target)
    {
        TargetToFollow = target;
        NextPosition = TargetToFollow.Position;
    }
}
