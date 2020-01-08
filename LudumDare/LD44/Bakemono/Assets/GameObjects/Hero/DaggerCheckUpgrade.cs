using UnityEngine;

public class DaggerCheckUpgrade : MonoBehaviour
{
    HeroStats _stats;

    private void OnEnable()
    {
        _stats = HeroStats.Get();

        if (!HeroStats.Get().Dagger)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.SetParent(null);
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _stats.transform.position + Vector3.up * 0.5f,
            _stats.MovementSpeed * 0.8f * Time.deltaTime);
    }
}
