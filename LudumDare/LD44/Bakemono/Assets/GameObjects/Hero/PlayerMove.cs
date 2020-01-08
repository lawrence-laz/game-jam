using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    public UnityEvent OnStartedMove;
    public UnityEvent OnStoppedMove;

    private HeroStats _stats;
    private Rigidbody2D _body;
    bool _moving = false;

    private void OnEnable()
    {
        _stats = GetComponent<HeroStats>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_moving && InputX.GetAxis().sqrMagnitude > 0.001)
        {
            _moving = true;
            OnStartedMove.Invoke();
        }
        else if (_moving && InputX.GetAxis().sqrMagnitude <= 0.001)
        {
            _moving = false;
            OnStoppedMove.Invoke();
        }

        _body.MovePosition(_body.position + InputX.GetAxis() * _stats.MovementSpeed * Time.deltaTime);
    }
}
