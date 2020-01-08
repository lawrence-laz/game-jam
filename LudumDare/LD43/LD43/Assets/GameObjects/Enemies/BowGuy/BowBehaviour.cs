using DG.Tweening;
using UnityEngine;

public class BowBehaviour : MonoBehaviour
{
    public GameObject ArrowPrefab;

    private NoticeBehaviour _notice;

    private void Start()
    {
        _notice = GetComponentInParent<NoticeBehaviour>();
    }

    public void ShootArrow(Transform target)
    {
        var arrow = Instantiate(ArrowPrefab, transform);
        arrow.transform.localPosition = Vector3.zero;

        var animation = DOTween.Sequence()
            .Append(arrow.transform.DOLocalMoveZ(-.5f, 1.7f))
            .AppendCallback(() =>
            {
                arrow.transform.SetParent(null);
                var arrowBehaviour = arrow.GetComponent<ArrowBehaviour>();
                arrowBehaviour.WhoShot = transform;
                var isInSight =_notice.IsInLineOfSight;
                if (isInSight)
                {
                    arrowBehaviour.KillerArrow = true;
                    arrowBehaviour.KillTarget = Camera.main.transform;
                }
                else
                {
                    arrow.transform.LookAt(_notice.LastSightPosition);
                }
                arrowBehaviour.Release();
            });
    }
}
