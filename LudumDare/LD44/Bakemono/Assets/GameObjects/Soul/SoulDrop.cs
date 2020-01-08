using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class SoulDrop : MonoBehaviour
{
    public int Count;
    public GameObject SoulPrefab;

    public void OnDeath()
    {
        DropSouls();
    }

    public void DropSouls()
    {
        for (int i = 0; i < Count; i+=5)
        {
            var soul = Instantiate(SoulPrefab, transform.position, Quaternion.identity);
            soul.transform.DOMove(Random.insideUnitCircle, 1).SetRelative(true).SetEase(Ease.OutElastic);
        }
    }

    public void DropSoulsWithCount(int count)
    {
        for (int i = 0; i < count; i += 5)
        {
            var soul = Instantiate(SoulPrefab, transform.position, Quaternion.identity);
            soul.transform.DOMove(Random.insideUnitCircle, 1).SetRelative(true).SetEase(Ease.OutElastic);
        }
    }
}
