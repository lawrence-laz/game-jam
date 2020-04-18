using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; set; }

    public GameObject DeadPrefab;
    public UnityEvent OnDeath;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Die()
    {
        OnDeath.Invoke();
        Instantiate(DeadPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
