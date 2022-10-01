using System;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[Serializable]
public class UnityEventString : UnityEvent<string> { }

public class Highscore : MonoBehaviour
{
    private int _oldScore;

    public int CurrentScore;
    public string LoggedInAs;
    public UnityEventString OnScoreChange;

    private void Start()
    {
        CurrentScore = 0;
        LogIn("anonymous", null);
    }

    private void Update() {
        if (_oldScore != CurrentScore)
        {
            _oldScore = CurrentScore;
            OnScoreChange.Invoke(CurrentScore.ToString());
        }
    }

    public void LogIn(string name, Action afterLogin)
    {
        if (LoggedInAs == name)
        {
            afterLogin?.Invoke();
            return;
        }

        var request = new LoginWithCustomIDRequest { CustomId = name, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, result =>
        {
            Debug.Log("Logged in");
            LoggedInAs = name;
            afterLogin?.Invoke();
        }, OnFail);
    }

    public void UploadHighscore(string name, Action afterUpload)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        },
        result =>
        {
            Debug.Log("Updated name");
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate> {
                        new StatisticUpdate {
                            StatisticName = "Highscore",
                            Value = CurrentScore,
                        }
                }
            };

            PlayFabClientAPI.UpdatePlayerStatistics(
                request,
                result =>
                {
                    Debug.Log("Updated leader boards");
                    afterUpload?.Invoke();
                },
                OnFail);
        },
        OnFail);
    }

    private void OnFail(PlayFabError error)
    {
        Debug.LogError("Playfab error: " + error.ErrorMessage);
        Debug.LogError(error.GenerateErrorReport());
    }
}
