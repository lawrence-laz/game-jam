using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RideAnimation : MonoBehaviour
{
    public float Magnitude = 10;
    public float Period = 1f / 3;
    public Sequence RunAnimation;
    public Sequence JumpAnimation;

    [Header("Jump")]
    public float JumpDuration = 1;
    public float JumpHeight = 2f;
    public float JumpSpeed = 0.1f;
    public bool CanJump = false;
    public float RoateDuringJump = 0;
    public Vector3 JumpDirection;
    public Transform JumpTransform;
    public UnityEvent OnJump;
    public UnityEvent OnLand;

    private void Start()
    {
        RunAnimation = DOTween.Sequence()
            .Append(transform.DOLocalRotate(Vector3.right * Magnitude, Period).SetRelative())
            .Append(transform.DOLocalRotate(-Vector3.right * Magnitude, Period).SetRelative())

            .SetLoops(-1, LoopType.Restart)
            .Play();

        if (JumpTransform == null)
        {
            JumpTransform = transform;
        }
    }

    private void Update()
    {
        if (CanJump && JumpAnimation == null && Input.GetKeyDown(KeyCode.Space))
        {
            RunAnimation.Pause();

            OnJump.Invoke();

            JumpAnimation = DOTween.Sequence()
                .Append(JumpTransform.DOLocalMove(JumpDirection * -JumpHeight * 0.1f, JumpSpeed / 2).SetRelative())
                .Append(JumpTransform.DOLocalMove(JumpDirection * JumpHeight * 1.1f, JumpSpeed).SetRelative())
                .Join(JumpTransform.DOLocalRotate(-Vector3.right * RoateDuringJump, JumpSpeed).SetRelative())
                .Append(JumpTransform.DOLocalMove(JumpTransform.localPosition, JumpDuration - JumpSpeed))
                .Join(JumpTransform.DOLocalRotate(Vector3.right * RoateDuringJump, JumpDuration - JumpSpeed).SetRelative())
                .AppendCallback(() =>
                {
                    JumpAnimation = null;
                    if (JumpTransform == null)
                    {
                        return;
                    }

                    OnLand.Invoke();
                    RunAnimation.Play();
                }).Play();
        }
    }

    private void OnDestroy()
    {
        if (RunAnimation != null)
        {
            RunAnimation.Kill();
        }
    }
}
