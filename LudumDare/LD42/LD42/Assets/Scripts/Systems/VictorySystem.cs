using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictorySystem : Singleton<VictorySystem>
{
    public GameObject VictoryUi;

    private SfxrSynth _victorySound;

    public bool Victory { get; private set; }

    private void OnEnable()
    {
        _victorySound = new SfxrSynth();
        _victorySound.parameters.SetSettingsString("0,.5,,.0488,.3076,.3505,.3,.6737,,,,,,,,,.5385,.6391,,,,,,,,1,,,,,,");
    }

    private void Start()
    {
        StartCoroutine(CheckForVictory());
    }

    //private void Update()
    //{
    //    if (GameOverSystem.Instance.GameOver || Victory)
    //        return;

    //    bool allEnemiesDead = GameObject.FindGameObjectsWithTag("Enemy")
    //        .All(x => x.GetComponent<HealthComponent>().Health <= 0);

    //    bool playerIsAlive = GameObject.FindGameObjectWithTag("Player")
    //        .GetComponent<HealthComponent>().Health > 0;

    //    if (allEnemiesDead && playerIsAlive)
    //    {
    //        Instantiate(VictoryUi);
    //        Victory = true;
    //    }
    //}

    private IEnumerator CheckForVictory()
    {
        bool allEnemiesDead;
        bool playerIsAlive;

        do
        {
            yield return new WaitForSeconds(1);

            allEnemiesDead = GameObject.FindGameObjectsWithTag("Enemy")
            .All(x => x.GetComponent<HealthComponent>().Health <= 0);

            playerIsAlive = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<HealthComponent>().Health > 0;

        } while (!playerIsAlive || !allEnemiesDead);

        if (!GameOverSystem.Instance.GameOver)
        {
            _victorySound.PlayMutated();
            Victory = true;
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            yield return new WaitForSeconds(1);
            Instantiate(VictoryUi);
        }
    }
}
