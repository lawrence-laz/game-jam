using DG.Tweening;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverUI;

    private void Start()
    {
        FindObjectOfType<GameManager>().OnGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var face = player.GetComponent<Face>();
        face.SetFace(face.GameOver);
        player.transform.rotation = Quaternion.identity;

        DOTween.KillAll();

        var sequence = DOTween.Sequence()
            .Append(player.transform.DOPunchScale(Vector3.up, 0.2f))
            .Append(player.transform.DOShakeRotation(0.2f, 20))
            .Append(player.transform.DORotate(Vector3.forward * -90, 1.5f).SetEase(Ease.InBack))
            .Join(player.transform.DOLocalMoveY(-2, 0.5f).SetDelay(1f))
            .AppendInterval(0.5f);

        var index = 0;
        foreach (var card in FindObjectsOfType<Card>())
        {
            sequence
                .Join(card.transform.DOMoveY(-3, 1).SetDelay(index * 0.2f))
                .Join(card.transform.DOLocalRotate(Vector3.forward * Random.Range(-15, 15), 0.6f));
        }

        sequence.AppendCallback(() => GameOverUI.SetActive(true));

        sequence
            .SetRelative(true)
            .Play();
    }
}
