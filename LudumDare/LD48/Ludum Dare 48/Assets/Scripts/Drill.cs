using UnityEngine;

public class Drill : MonoBehaviour
{
    public bool IsDrilling;
    public GameObject Target;
    public float DrillingSpeed; // Amount per second.

    private Lander _lander;
    private Storage _storage;

    private void Start()
    {
        _lander = transform.parent.GetComponentInChildren<Lander>();
        _lander.OnLand.AddListener(OnLand);
        _lander.OnLiftOff.AddListener(OnLiftOff);
        gameObject.SetActive(false);
        _storage = transform.parent.GetComponentInChildren<Storage>();
        transform.parent.GetComponent<Health>().OnDead.AddListener(OnDead);
    }

    private void OnDead()
    {
        enabled = false;
    }

    private void Update()
    {
        if (IsDrilling)
        {
            if (!_storage.HasSpace)
            {
                StopDrilling();
            }
            else if (Target.TryGetComponent<Asteroid>(out var asteroid) && asteroid.CurrentValue > 0)
            {
                var drillAmount = Mathf.Min(DrillingSpeed * Time.deltaTime * asteroid.SpeedMultiplier, asteroid.CurrentValue);
                _storage.CurrentValue += drillAmount;
                asteroid.CurrentValue -= drillAmount;
            }
        }
    }

    private bool CanMine(GameObject target)
    {
        return target.TryGetComponent<Asteroid>(out var asteroid) && asteroid.CurrentValue > 0;
    }

    private void OnLiftOff()
    {
        StopDrilling();
    }

    private void StopDrilling()
    {
        IsDrilling = false;
        gameObject.SetActive(false);
    }

    private void OnLand(GameObject target)
    {
        if (CanMine(target))
        {
            StartDrilling(target);
        }
    }

    private void StartDrilling(GameObject target)
    {
        Target = target;
        IsDrilling = true;
        gameObject.SetActive(true);
    }
}
