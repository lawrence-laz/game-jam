using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntranceUnlock : MonoBehaviour
{
    public GameObject LockedSprite;
    public GameObject UnlockedSprite;
    public int SoulsToUnlock;
    public UnityEvent OnUnlocked;
    public Text SoulsToUnlockText;

    private HeroStats _stats;
    private SpawnerSpawn[] _spawners;

    private void OnEnable()
    {
        _stats = HeroStats.Get();
        SoulsToUnlockText.text = SoulsToUnlock != -1 ? SoulsToUnlock.ToString() + "\nSOUL" : string.Empty;
        _spawners = FindObjectsOfType<SpawnerSpawn>();
        InvokeRepeating("SlowerUpdate", .5f, .5f);
    }

    private void SlowerUpdate()
    {
        if (SoulsToUnlock != -1 && _stats.Health < SoulsToUnlock)
        {
            return;
        }
        else if (SoulsToUnlock == -1)
        {
            foreach (var spawner in _spawners)
            {
                if (spawner.SpawnLimit > 0)
                {
                    return;
                }
            }
            if (FindObjectsOfType<Monster>().Length > 0)
            {
                return;
            }
        }

        if (SceneManager.GetActiveScene().name == "Shop")
        {
            return;
        }

        UnlockedSprite.SetActive(true);
        LockedSprite.SetActive(false);
        SoulsToUnlockText.enabled = false;
        OnUnlocked.Invoke();
        Destroy(this);
    }
}
