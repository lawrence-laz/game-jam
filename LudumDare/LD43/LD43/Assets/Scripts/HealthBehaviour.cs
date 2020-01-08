using UnityEngine;
using UnityEngine.Events;
using UnityGoodies;

public class HealthBehaviour : MonoBehaviour
{
    public float MaxHealth;
    public Transform LastDamager;

    [SerializeField]
    private float _health;

    public UnityEvent OnDeath;

    public float Health
    {
        get { return _health; }
        set
        {
            var previousHealth = _health;
            _health = Mathf.Max(0, value);

            if (previousHealth > 0 && _health == 0)
            {
                this.ForAllComponentsInRootsChildren<Rigidbody>(
                    body => body.isKinematic = false
                );
                OnDeath.Invoke();
            }
        }
    }

    public bool IsAlive { get { return _health > 0; } }

    private void Start()
    {
        _health = MaxHealth;
    }
}
