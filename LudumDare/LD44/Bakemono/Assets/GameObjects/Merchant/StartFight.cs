using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFight : MonoBehaviour
{
    public Sprite UpArms;

    SpawnerSpawn[] _spawners;

    private void OnEnable()
    {
        _spawners = FindObjectsOfType<SpawnerSpawn>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Level11")
            return;

        foreach (var spawner in _spawners)
        {
            if (spawner.SpawnLimit > 0)
                return;
        }

        foreach (var monster in FindObjectsOfType<Monster>())
        {
            if (monster != GetComponent<Monster>())
                return;
        }

        transform.Find("RedHead1_Sprite").GetComponent<SpriteRenderer>().sprite = UpArms;
        GetComponent<SlowEnemyFollow>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<ArroundProjectileShooter>().enabled = false;
        GetComponent<HornayNailsShooter>().enabled = true;
        var stats = GetComponent<EnemyStats>();
        stats.ProjectilesSpeed = 10;
        stats.ProjectilesCount = 30;
        stats.ProjectilesAuthoShootPeriod = 2f;
        stats.Damage = 20;

        HeroStats.Get().Health -= 50;

        Destroy(this);
    }
}
