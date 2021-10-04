using DG.Tweening;
using System.Threading;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Transform IdleGrip;
    public Transform LeftAttackGrip;
    public Transform RightAttackGrip;
    public Transform Aim; // For hitting.
    public Transform Head;
    public float RotationSpeed = 120;
    public float FollowSpeed = 2;
    public GameObject HitEffect;

    public Sequence AttackAnimation;

    private void FixedUpdate()
    {
        if (AttackAnimation != null)
        {
            return;
        }
        else if (Input.GetMouseButton(0))
        {
            var headRotation = Head.localEulerAngles.y > 180
                ? Head.localEulerAngles.y - 360
                : Head.localEulerAngles.y;

            if (headRotation > 0)
            {
                UpdateGrip(RightAttackGrip);
            }
            else
            {
                UpdateGrip(LeftAttackGrip);
            }
        }
        else
        {
            UpdateGrip(IdleGrip);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.collider.gameObject.layer) & LayerMask.GetMask("Enemy")) != 0)
        {
            Debug.Break();
            PlayAttackAnimation(collision.contacts[0].point, collision.collider);
        }
    }

    private void UpdateGrip(Transform targetGrip)
    {
        var rotationSpeed = targetGrip == IdleGrip
            ? RotationSpeed
            : RotationSpeed * 3;
        var followSpeed = targetGrip == IdleGrip
            ? FollowSpeed
            : FollowSpeed * 3;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetGrip.rotation, rotationSpeed * Time.fixedDeltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetGrip.position, followSpeed * Time.fixedDeltaTime);
    }

    public void PlayAttackAnimation(Vector3 contact, Collider collider)
    {
        if (AttackAnimation != null)
        {
            return;
        }

        collider.gameObject.layer = 0;

        Debug.Log("hello");

        var localPositionTarget = transform.parent.InverseTransformPoint(contact - new Vector3(0.7f, 0.5f, 2.8f));
        var originalPosition = collider.transform.position;

        AttackAnimation = DOTween.Sequence()
            .Append(transform.DOLocalMove(localPositionTarget, 0.05f))
            .Join(transform.DOLocalRotate(new Vector3(432.437f, 49.675f, -52.137f), 0.05f))
            .AppendCallback(() =>
            {
                var offset = collider != null
                    ? collider.transform.position - originalPosition
                    : Vector3.zero;
                Instantiate(HitEffect, contact + offset, Quaternion.identity);
                FindObjectOfType<ScreenShake>().MediumShake();
                FindObjectOfType<ScoreManager>().CurrentScore += 1;

                AttackAnimation = DOTween.Sequence()
                    .Append(transform.DOMove(contact + offset, 0.08f))
                    .Join(transform.DOLocalRotate(new Vector3(409.282f, 126.496f, 56.01f), 0.08f))
                    .AppendCallback(() =>
                    {
                        Thread.Sleep(20);
                        AttackAnimation = null;
                        if (collider != null)
                            Destroy(collider.gameObject);
                    });
            })
            .Play();
    }
}
