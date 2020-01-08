using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class GameOverSystem : Singleton<GameOverSystem>
{
    public GameObject GameOverUi;

    private HealthComponent _player;

    private SfxrSynth _gameOverSound;
    private const string GameOverSound = "3,.5,,.3174,.7051,.298,.3,.0671,,-.3428,,,,,,,,,,,,,,.0194,-.2021,1,,,,,,";

    public bool GameOver { get; private set; }

    private void Start()
    {
        _gameOverSound = new SfxrSynth();
        _gameOverSound.parameters.SetSettingsString(GameOverSound);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();

        StartCoroutine(TestForGameOver());
    }

    private GameObject _gameOverInstance = null;
    private IEnumerator TestForGameOver()
    {
        while (!VictorySystem.Instance.Victory && _player.Health > 0)
            yield return new WaitForSeconds(0.1f);

        try
        {
            if (_player.Health <= 0 && !VictorySystem.Instance.Victory)
            {
                Camera.main.transform.DOShakePosition(0.4f, 0.3f, 30, 90, false, true);
                Camera.main.transform.DOShakeRotation(0.4f, 0.3f, 30, 90, true);

                GameOver = true;
                _gameOverSound.Play();
                yield return new WaitForSeconds(1);
                _gameOverInstance = Instantiate(GameOverUi);
            }
        }
        finally
        {
            if (_gameOverInstance == null && !VictorySystem.Instance.Victory && _player.Health == 0)
                _gameOverInstance = Instantiate(GameOverUi);
        }
    }
}
