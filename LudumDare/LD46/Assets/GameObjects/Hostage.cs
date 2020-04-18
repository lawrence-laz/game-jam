using System;
using UnityEngine;

public class Hostage : MonoBehaviour
{
    public TurnManager TurnManager { get; set; }
    public Move Move { get; set; }
    public Transform TransformToFollow { get; set; }
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
        if (TransformToFollow == null) return;

        if (Map.Get(NextPosition)?.tag == "Player") return;

        Move.MoveTo(NextPosition);
    }

    private void OnTurnEnded()
    {
        if (TransformToFollow == null) return;

        NextPosition = TransformToFollow.position;
    }

    public void Follow(Transform transform)
    {
        TransformToFollow = transform;
        NextPosition = TransformToFollow.position;
    }
}
