using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public Conveyor Conveyor;
    public Transform Player;
    public float TurnSpeed;

    private void Start()
    {
        Conveyor = GetComponent<Conveyor>();
        Player = FindObjectOfType<PlayerController>().transform;
    }

    private void FixedUpdate()
    {
        Conveyor.OverrideDirection = true;
        //var targetPosition = Random.value >= 0.5f
        //    ? Player.position + Player.right * 0.5f
        //    : Player.position - Player.right * 0.5f;
        var newDirection = Vector3.MoveTowards(Conveyor.DirectionModifier, Player.position - transform.position, TurnSpeed * Time.fixedDeltaTime).normalized;
        newDirection.y = 0;
        Conveyor.DirectionModifier = newDirection;

        //transform.LookAt(Player);
    }
}
