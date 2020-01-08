using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnityIntEvent : UnityEvent<int> { }

public class HeroStats : MonoBehaviour
{
    public UnityEvent OnShieldGained;
    public UnityEvent OnShieldLost;
    public UnityEvent OnShieldDamage;

    public float ArrowStretchTime = 2;
    public float ArrowMaxSpeed = 5;
    public float MovementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }
    public int Shield
    {
        get { return _shield; }
        set
        {
            if (_shield > value) // Damage
            {
                if (_shield == 0) // Don't have already
                {
                    return;
                }

                if (value == 0) // Lost
                {
                    OnShieldLost.Invoke();
                }
                else // Damage
                {
                    OnShieldDamage.Invoke();
                }
            }
            else // Gain
            {
                if (_shield <= 0) // Gained from 0
                {
                    OnShieldGained.Invoke();
                }
            }

            _shield = value;
        }
    }
    public bool HasShield { get { return _hasShield; } set { _hasShield = value; } }
    public int ArrowDamage = 1;
    public float ArrowPushback = 30;
    public float InvoAfterDamage = 0.5f;

    public UnityEvent OnDamaged;
    public UnityIntEvent OnDamagedWithValue;

    private float _lastTimeDamaged;

    public int Health
    {
        get { return _health; }
        set
        {
            if (_health > value)
            {
                if (Time.time - _lastTimeDamaged < InvoAfterDamage)
                {
                    return;
                }

                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Hero"), LayerMask.NameToLayer("Enemy"));
                DOTween.Sequence()
                    .AppendInterval(InvoAfterDamage * 0.9f)
                    .AppendCallback(() => Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Hero"), LayerMask.NameToLayer("Enemy"), false));

                _lastTimeDamaged = Time.time;
                if (Shield > 0)
                {
                    Shield -= 1;
                    return;
                }

                OnDamaged.Invoke();
                OnDamagedWithValue.Invoke(_health - value);
            }
            _health = value;
        }
    }

    public int DirectHealth
    {
        get { return _health; }
        set
        {
            if (_health > value)
            {
                OnDamaged.Invoke();
                OnDamagedWithValue.Invoke(_health - value);
            }
            _health = value;
        }
    }

    public bool MultiShot
    {
        get { return _multiShot; }
        set { _multiShot = value; }
    }

    public bool AutoAim
    {
        get { return _autoAim; }
        set { _autoAim = value; }
    }

    public bool BounceArrows
    {
        get { return _bounceArrows; }
        set { _bounceArrows = value; }
    }
    public bool Dagger
    {
        get { return _dagger; }
        set { _dagger = value; }
    }

    public static bool _bounceArrows;
    public static bool _dagger;
    public static bool _autoAim;
    public static bool _multiShot;
    public static int _health;
    public static float _movementSpeed;
    public static int _shield;
    public static bool _hasShield;

    public static HeroStats Get()
    {
        return GameObject.FindWithTag("Player").GetComponent<HeroStats>();
    }

    static HeroStats()
    {
        Reset();
    }

    public static void Reset()
    {
        _health = 50;
        _movementSpeed = 6;
        _shield = 0;
        _hasShield = false;
        _multiShot = false;
        _autoAim = false;
        _bounceArrows = false;
        _dagger = false;
    }

    [ContextMenu("EnableDagger")]
    public void EnableDagger()
    {
        Dagger = true;
    }

    [ContextMenu("EnableBounceArrows")]
    public void EnableBounceArrows()
    {
        BounceArrows = true;
    }

    [ContextMenu("EnableTripleShot")]
    public void EnableTripleShot()
    {
        MultiShot = true;
    }

    [ContextMenu("EnableAutoAim")]
    public void EnableAutoAim()
    {
        AutoAim = true;
    }

    [ContextMenu("EnableShield")]
    public void EnableShield()
    {
        HasShield = true;
    }

    [ContextMenu("EnableMovement")]
    public void EnableMovement()
    {
        MovementSpeed *= 1.6f;
    }


    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
            Health += 100;

        if (Input.GetKeyDown(KeyCode.Space))
            Health = 0;
#endif
    }
}
