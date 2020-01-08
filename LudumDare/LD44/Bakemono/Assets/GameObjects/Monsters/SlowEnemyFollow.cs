using UnityEngine;

public class SlowEnemyFollow : MonoBehaviour
{
    private Transform _player;
    private EnemyStats _stats;
    private Rigidbody2D _body;

    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _stats = GetComponent<EnemyStats>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveTowardsHeroInStraightLine();
    }

    private void MoveTowardsHeroInStraightLine()
    {
        var nextPosition = Vector2.MoveTowards(_body.position, _body.position + (Vector2)transform.DirectionTo(_player), _stats.MovementSpeed * Time.deltaTime);
        _body.MovePosition(nextPosition);
    }
}
