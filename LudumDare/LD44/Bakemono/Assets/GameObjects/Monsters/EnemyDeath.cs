using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class EnemyDeath : MonoBehaviour
{
    public UnityEvent OnDeath;

    private EnemyStats _stats;

    private void OnEnable()
    {
        _stats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        DieWhenHealthZeroOrBellow();
    }

    private void DieWhenHealthZeroOrBellow()
    {
        if (_stats.Health <= 0)
        {
            gameObject.SendMessage("OnDeath");
            OnDeath.Invoke();
        }
    }
}
