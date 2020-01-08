using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityGoodies;

public class SwordBehaviour : MonoBehaviour
{
    public PlayRandomClipBehaviour OnHitSound;

    private Transform _model;
    private Transform _wielder;

    public UnityEvent OnSwoosh;

    public void Attack(Vector3 position, Transform target)
    {
        Debug.LogFormat("{0} is attacking {1}", gameObject.name, target.gameObject.name);
        target.ForAllComponentsInRootsChildren<HealthBehaviour>(hp =>
        {
            hp.Health = 0;
            hp.LastDamager = _wielder;
        });

        transform.parent.SetParent(target, true);
        var direction = transform.InverseTransformDirection(transform.position.DirectionTo(position));

        var animation = DOTween.Sequence()
            .Append(_model.DOLocalMove(transform.localPosition - direction * 0.6f, .25f).SetEase(Ease.OutCubic))
            .AppendCallback(() => OnSwoosh.Invoke())
            .Append(_model.DOLocalMove(transform.InverseTransformPoint(position), .15f).SetEase(Ease.OutCubic))
            .Join(_model.DOLookAt(position, 0.24f))
            //.Join(Camera.main.transform.DOShakePosition(
            //    duration: 0.5f,
            //    strength: 0.3f,
            //    fadeOut: true))
            .AppendCallback(() =>
            {
                if (OnHitSound)
                {
                    OnHitSound.Play();
                }
            });
    }

    private void Start()
    {
        _model = transform.GetChild(0);
        _wielder = transform.parent;
    }
}
