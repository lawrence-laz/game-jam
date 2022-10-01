using DG.Tweening;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public GameObject UpgradeMarker;
    public GameObject UpgradePrefab;

    private Sequence _animation;

    void Start()
    {
        if (UpgradePrefab)
        {
            EnableMarker();
        }
    }

    private void OnDestroy()
    {

        _animation.Kill();
        if (UpgradePrefab != null && gameObject.scene.isLoaded)
        {
            Instantiate(UpgradePrefab, transform.position, Quaternion.identity);
        }
    }

    private void EnableMarker()
    {
        UpgradeMarker.SetActive(true);
        _animation = DOTween.Sequence()
            .Append(UpgradeMarker.transform.DORotate(-Vector3.forward * 45, 0.6f))
            .Append(UpgradeMarker.transform.DORotate(Vector3.forward * 45, 0.6f))
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void AddUpgrade(GameObject upgradePrefab)
    {
        UpgradePrefab = upgradePrefab;
        EnableMarker();
    }
}
