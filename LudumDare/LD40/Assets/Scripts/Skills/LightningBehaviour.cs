using UnityEngine;

namespace Assets.Scripts
{
    public class LightningBehaviour : SkillBehaviour
    {
        protected override void ShootLogic(Vector3 position)
        {
            ShootLightning(position);

            base.ShootLogic(position);
        }

        private void ShootLightning(Vector3 position)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(position, radius*(Time.timeSinceLevelLoad < 20 ? 1.5f : 2f), Vector3.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.tag == "Zombie")
                {
                    hit.transform.GetComponent<DeathBehaviour>().Die();
                    continue;
                }

                HealthBehaviour health = hit.collider.GetComponent<HealthBehaviour>();
                if (health == null)
                    continue;

                health.Health = 0;
            }
        }
    }
}
