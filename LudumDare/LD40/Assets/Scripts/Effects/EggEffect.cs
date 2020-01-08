using System.Collections;
using UnityEngine;

public class EggEffect : EffectBehaviour
{
    [SerializeField]
    private GameObject eggEffect;

    public override IEnumerator PreShotEffect(Vector3 position)
    {
        eggEffect.SetActive(true);
        eggEffect.transform.position = position;
        Animator animator = eggEffect.GetComponent<Animator>();
        animator.Play("Wiggle");
        yield return animator.WhilePlaying();
        yield return new WaitForSeconds(1);
        animator.Play("Wiggle");
        yield return animator.WhilePlaying();
        animator.Play("Crack");
        yield return new WaitForSeconds(0.4f);
    }

    public override IEnumerator PostShotEffect(Vector3 position)
    {
        Animator animator = eggEffect.GetComponent<Animator>();
        animator.Play("Born");
        yield return animator.WhilePlaying();
        eggEffect.SetActive(false);

        yield return base.PostShotEffect(position);
    }
}
