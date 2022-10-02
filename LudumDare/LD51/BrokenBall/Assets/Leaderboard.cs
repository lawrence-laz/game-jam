using System.Threading;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public Transform[] Rows;

    private void OnEnable()
    {
        Debug.Log("VISIBLE");
        Reload();
        InvokeRepeating(nameof(Reload), 5, 5);
    }

    public void Reload()
    {
        var highscore = FindObjectOfType<Highscore>();
        highscore.LogIn("anonymous", () =>
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "Highscore",
                StartPosition = 0,
                MaxResultsCount = 5,
            };
            PlayFabClientAPI.GetLeaderboard(
                request,
                result =>
                {
                    Debug.Log("Updating leaderboard");
                    for (var i = 0; i < 5; ++i)
                    {
                        var row = Rows[i];
                        if (result.Leaderboard.Count > i)
                        {
                            var item = result.Leaderboard[i];
                            row.Find("Name").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
                            row.Find("Score").GetComponent<TextMeshProUGUI>().text = item.StatValue.ToString();
                        }
                        else
                        {
                            row.Find("Name").GetComponent<TextMeshProUGUI>().text = "-";
                            row.Find("Score").GetComponent<TextMeshProUGUI>().text = "-";
                        }
                    }
                },
                error => Debug.LogError(error.GenerateErrorReport()));
        });
    }
}
