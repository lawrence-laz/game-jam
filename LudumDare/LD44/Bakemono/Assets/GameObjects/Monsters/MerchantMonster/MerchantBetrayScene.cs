using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MerchantBetrayScene : MonoBehaviour
{
    public Sprite HandsUp;

    SpriteRenderer _sprite;

    private void OnEnable()
    {
        _sprite = transform.parent.Find("RedHead1_Sprite").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Level10")
            Destroy(gameObject);
    }

    public void Begin()
    {
        GameObject.Find("Melody_AudioSource").SetActive(false);

        
        DOTween.Sequence()
            .AppendInterval(1)
            .AppendCallback(() => transform.Find("Laugh_Audio").GetComponent<AudioSource>().Play())
            .AppendInterval(4)
            .AppendCallback(() =>
            {
                _sprite.sprite = HandsUp;
                transform.Find("Spell_Audio").GetComponent<AudioSource>().Play();
            })
            .Append(_sprite.transform.DOMove(Vector3.up * 2, 3).SetEase(Ease.InQuad).SetRelative(true))
            .AppendInterval(4.5f)
            .AppendCallback(() => {
                HeroStats.Get().DirectHealth = 40;
            })
            .AppendInterval(1)
            .AppendCallback(() => ScreenFade.Instance.FadeOut(0.2f))
            .AppendInterval(0.2f)
            .AppendCallback(()=> SceneManager.LoadScene("Level11"))
            .SetUpdate(true);
    }
}
