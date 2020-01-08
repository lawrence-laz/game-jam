using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    private void Update()
    {
        foreach (var projectile in FindObjectsOfType<ProjectileComponent>())
        {
            if (!projectile.IsShot)
                continue;

            projectile.transform.localPosition += projectile.transform.up * projectile.Speed * Time.deltaTime;
        }
    }
}
