using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class SleepEffect : EffectBehaviour
    {
        [SerializeField]
        private GameObject blueExplosion;

        public override IEnumerator PreShotEffect(Vector3 position)
        {
            yield return new WaitForEndOfFrame();
        }

        public override IEnumerator PostShotEffect(Vector3 position)
        {
            blueExplosion.SetActive(true);
            blueExplosion.transform.position = position;
            Animator animator = blueExplosion.GetComponent<Animator>();
            animator.Play("GreenExplosion");
            yield return animator.WhilePlaying();
            blueExplosion.SetActive(false);
        }
    }
}
