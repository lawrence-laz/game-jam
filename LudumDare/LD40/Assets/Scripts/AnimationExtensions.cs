using System.Collections;
using UnityEngine;

public static class AnimationExtensions
{
    public static IEnumerator WhilePlaying(this Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }
}

public static class AnimatorExtensions
{
    public static IEnumerator WhilePlaying(this Animator animator)
    {
        do
        {
            yield return null;
        } while (animator.IsPlaying());
    }

    public static bool IsPlaying(this Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}

