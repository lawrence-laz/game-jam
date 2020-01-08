using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour
{
    public bool ShouldGoToShop = false;

    public static int NextLevel
    {
        get { return PlayerPrefs.GetInt("NEXT_LEVEL", 1); }
        set { PlayerPrefs.SetInt("NEXT_LEVEL", value); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag != "Player")
        {
            return;
        }

        enabled = false;
        ScreenFade.Instance.FadeOut(.4f);
        GameObject.FindWithTag("Player").GetComponent<Collider2D>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().enabled = false;

        GetComponent<AudioSource>().Play();
        GetComponent<Collider2D>().enabled = false;
        DOTween.Sequence()
            .AppendInterval(2)
            .AppendCallback(() => LoadNextLevel())
            .SetUpdate(true);
    }

    private void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name != "Shop")
        {
            NextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            if (ShouldGoToShop)
            {
                Debug.Log("Going to level: Shop");
                DOTween.Clear(true);
                SceneManager.LoadScene("Shop");
            }
            else
            {
                Debug.Log("Going to level: " + NextLevel);
                DOTween.Clear(true);
                SceneManager.LoadScene(NextLevel);
            }
        }
        else
        {
            Debug.Log("Going to level: " + NextLevel);
            DOTween.Clear(true);
            SceneManager.LoadScene(NextLevel);
        }

    }
}
