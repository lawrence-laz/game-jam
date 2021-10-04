using DG.Tweening;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public GameObject GameOverUI;

    public void OnDeath()
    {
        var player = GetComponent<PlayerController>();
        player.Horse.GetComponent<Conveyor>().enabled = true;
        player.enabled = false;
        player.GetComponentInChildren<MouseLook>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponentInChildren<RideAnimation>().RunAnimation.Kill();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DOTween.Sequence()
            .Append(player.transform.DOMoveY(0, 0.2f))
            .Join(player.transform.DOLookAt(player.transform.position + Vector3.up * 3, 1))
            .AppendCallback(() => GameOverUI.SetActive(true))
            .Play();
        GameObject.Find("dot").SetActive(false);
        var axe = player.transform.Find("axe");
        axe.SetParent(null);
        axe.gameObject.AddComponent<Rigidbody>();
        foreach (var spawner in FindObjectsOfType<Spawner>())
        {
            spawner.enabled = false;
        }

        ScoreManager score = FindObjectOfType<ScoreManager>();
        if (score.BestScore < score.CurrentScore)
            score.BestScore = score.CurrentScore;
    }
}
