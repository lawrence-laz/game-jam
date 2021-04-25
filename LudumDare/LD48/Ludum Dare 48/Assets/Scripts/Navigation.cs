using Libs.Base.Extensions;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public float RotationPower;
    public float MovePower;
    public float MaxSpeed = 3;

    public float TopEnginesPower => _currentMovePower.NegativeAbs();
    public float BotLeftEnginePower => _currentRotationPower.NegativeAbs() / RotationPower + _currentMovePower.PositiveAbs() / MovePower;
    public float BotRightEnginePower => _currentRotationPower.PositiveAbs() / RotationPower + _currentMovePower.PositiveAbs() / MovePower;

    private float _currentRotationPower;
    private float _currentMovePower;

    private Rigidbody2D _body;
    private Lander _lander;
    private Fuel _fuel;

    public void Rotate(float direction)
    {
        if (_fuel.CurrentValue <= 0 || !enabled)
        {
            return;
        }

        if (_lander.IsLanded)
        {
            return;
        }

        _currentRotationPower = direction * -RotationPower;
    }

    public void Move(float direction)
    {
        if (_fuel.CurrentValue <= 0 || !enabled)
        {
            return;
        }

        if (direction > 0 && _lander.IsLanded)
        {
            _lander.Detach();
        }

        _currentMovePower = direction * MovePower;
    }

    private void Start()
    {
        _body = transform.parent.GetComponent<Rigidbody2D>();
        _lander = transform.parent.GetComponentInChildren<Lander>();
        _lander.OnLand.AddListener(OnLand);
        _fuel = GetComponent<Fuel>();

        transform.parent.GetComponent<Health>().OnDead.AddListener(OnDead);
    }

    private void OnDead()
    {
        enabled = false;
    }

    private void OnLand(GameObject _)
    {
        StopThrusters();
    }

    private void Update()
    {
        UpdatePhysics();
        UpdateFuel();
    }

    private void UpdateFuel()
    {
        _fuel.CurrentValue -= Time.deltaTime * Mathf.Abs(_currentRotationPower) / RotationPower / 2;
        _fuel.CurrentValue -= Time.deltaTime * Mathf.Abs(_currentMovePower) / MovePower;
    }

    private void UpdatePhysics()
    {
        if (_fuel.CurrentValue <= 0)
        {
            StopThrusters();
            return;
        }

        _body.AddForce(transform.up * _currentMovePower, ForceMode2D.Force);
        _body.AddTorque(_currentRotationPower, ForceMode2D.Force);

        if (_body.velocity.magnitude >= MaxSpeed)
        {
            _body.velocity = _body.velocity.normalized * MaxSpeed;
        }
    }

    private void StopThrusters()
    {
        _currentRotationPower = 0;
        _currentMovePower = 0;
    }
}
