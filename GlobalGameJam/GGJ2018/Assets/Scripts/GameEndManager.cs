using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    private void Start()
    {
        CancelInvoke();
        InvokeRepeating("CheckForGameEnd", 2, 2);
    }

    private void CheckForGameEnd()
    {
        if (Coin.GetAllActive().Count > 0)
            return;

        if (HardwareManager.IsInWinCondition)
        {
            SceneManager.LoadScene("Won");
        }
        else
        {
            SceneManager.LoadScene("Lost");
        }
    }
}
