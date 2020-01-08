using DG.Tweening;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityGoodies;

public class StabBehaviour : MonoBehaviour
{
    public Collider PickupCollider;
    public float Power;
    public float StabDistance = 3;
    public PlayRandomClipBehaviour OnFleshHitSound;

    private Vector3 _startingLocalRotation;
    private Vector3 _startingLocalPosition;

    public UnityEvent OnSwoosh;

    private void Start()
    {
        _startingLocalPosition = transform.localPosition;
        _startingLocalRotation = transform.localRotation.eulerAngles;
    }

    public void Stab(Vector3 position, Transform target)
    {
        if (position.DistanceTo(Camera.main.transform.position) > StabDistance)
        {
            Debug.LogFormat("Stabbing too far at {0}", position);
            Stab();
            return;
        }

        position += Camera.main.transform.forward * 0.1f;

        var shouldGetCharge = target.GetComponent<HealthBehaviour>().Health > 0;

        target.ForAllComponentsInRootsChildren<Rigidbody>(
            body => body.isKinematic = false
        );
        OnSwoosh.Invoke();

        Debug.LogFormat("Stabbing at {0}", position);
        var animation = DOTween.Sequence()
            .Append(transform.DOMove(position, 0.2f).SetEase(Ease.OutCubic))
            .Join(transform.DOLookAt(position, 0.2f))
            .Join(Camera.main.transform.DOShakePosition(
                duration: 0.1f,
                strength: 0.1f,
                fadeOut: false))
            .AppendCallback(() =>
            {
                target.ForAllComponentsInRootsChildren<HealthBehaviour>(
                    hp => hp.Health = 0
                );
                if (OnFleshHitSound)
                {
                    OnFleshHitSound.Play();
                }

                transform.parent.SetParent(target, true);
                PickupCollider.enabled = true;
                target.ForAllComponentsInRootsChildren<Rigidbody>(
                    body => body.AddForceAtPosition(transform.forward * Power, position)
                );
                Thread.Sleep(10);
                if (shouldGetCharge)
                {
                    Debug.Log("Getting a charge");
                    GameObject.FindGameObjectWithTag("Player").transform.ForAllComponentsInChildren<BoltShooterBehaviour>(
                        shooter => shooter.IncreaseCharge());
                }
            });
    }

    public void Stab(Vector3 position)
    {
        position += Camera.main.transform.forward * 0.1f;

        OnSwoosh.Invoke();

        Debug.LogFormat("Stabbing at {0}", position);
        var animation = DOTween.Sequence()
            .Append(transform.DOMove(position, 0.2f).SetEase(Ease.OutCubic))
            .Join(transform.DOLookAt(position, 0.2f))
            .AppendCallback(() =>
            {
                Thread.Sleep(10);
            })
            .AppendInterval(0.09f)
            .Append(transform.DOLocalMove(_startingLocalPosition, .2f))
            .Join(transform.DOLocalRotate(_startingLocalRotation, .2f));
    }

    public void Stab()
    {
        Stab(Camera.main.transform.position + Camera.main.transform.forward * 2 + Vector3.up * .1f);
    }
}
