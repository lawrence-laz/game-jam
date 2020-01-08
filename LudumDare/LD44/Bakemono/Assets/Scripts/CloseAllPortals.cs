using DG.Tweening;
using UnityEngine;

public class CloseAllPortals : MonoBehaviour
{
    public void ClosePortals()
    {
        foreach (var obj in FindObjectsOfType<SpawnerSpawn>())
        {
            obj.MonstersOnScreenLimit = 0;
            DOTween.Sequence()
                .Append(obj.transform.DOScale(0, Random.Range(0.5f, 1f)))
                .AppendCallback(() => Destroy(obj.gameObject));
        }
    }
}
