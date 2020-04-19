using DG.Tweening;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    public TileObject TileObject { get; set; }
    public Death Death { get; set; }

    private Sequence _animation;

    private void Start()
    {
        TileObject = GetComponent<TileObject>();
        TileObject.OnStepped.AddListener(OnStepped);
        Death = GetComponent<Death>();

        _animation = DOTween.Sequence()
            .Append(transform.DOShakePosition(0.2f, 0.05f, 1, 0))
            .SetLoops(-1);
    }

    private void OnDestroy()
    {
        if (_animation != null)
        {
            _animation.Kill();
        }
    }

    private void OnStepped(GameObject[] steppedBy)
    {
        Death.Die();
    }
}
