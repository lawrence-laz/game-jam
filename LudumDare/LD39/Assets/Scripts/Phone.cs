using UnityEngine;
using UnityEngine.SceneManagement;

public class Phone : MonoBehaviour
{
    [SerializeField]
    Sprite main;
    [SerializeField]
    Sprite phonesung;
    [SerializeField]
    Sprite off;

    [SerializeField]
    Sprite charging;
    [SerializeField]
    Sprite no9;
    [SerializeField]
    Sprite no91;
    [SerializeField]
    Sprite no911;
    [SerializeField]
    Sprite calling;
    [SerializeField]
    Sprite low;

    void TurnOn()
    {
        CancelInvoke();
        ChangeToPhonesung();
        Invoke("ChangeToCharging", 2);
    }

    void PlayPlug()
    {
        SoundPlayer.Instance.PlayPlug();
    }

    void Call911()
    {
        CancelInvoke();
        ChangeToNo9();
        Invoke("ChangeToNo91", 0.5f);
    }

    void TurnOff()
    {
        CancelInvoke();
        ChangeToPhonesung();
        Invoke("ChangeToOff", 2);
    }

    #region TurnOnAndCall911
    void ChangeToCharging()
    {
        GetComponent<SpriteRenderer>().sprite = charging;
    }

    void ChangeToNo9()
    {
        GetComponent<SpriteRenderer>().sprite = no9;
    }
    void ChangeToNo91()
    {
        GetComponent<SpriteRenderer>().sprite = no91;
        Invoke("ChangeToNo911", 0.5f);
    }
    void ChangeToNo911()
    {
        GetComponent<SpriteRenderer>().sprite = no911;
        Invoke("ChangeToCalling", 1f);
    }
    void ChangeToCalling()
    {
        GetComponent<SpriteRenderer>().sprite = calling;
        SoundPlayer.Instance.PlayCalling();
        Invoke("Answer", 7);
    }
    void Answer()
    {
        SoundPlayer.Instance.PlayAnswer();
        Invoke("LoadCredits", 2);
    }

    void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    #endregion

    void ChangeToMain()
    {
        GetComponent<SpriteRenderer>().sprite = main;
    }
    void ChangeToPhonesung()
    {
        GetComponent<SpriteRenderer>().sprite = phonesung;
    }
    void ChangeToOff()
    {
        GetComponent<SpriteRenderer>().sprite = off;

        foreach (Light light in transform.parent.GetComponentsInChildren<Light>())
        {
            if (light.type == LightType.Spot)
                light.enabled = false;
        }
    }
    void ChangeToLow()
    {
        SoundPlayer.Instance.PlayLow();
        GetComponent<SpriteRenderer>().sprite = low;
    }
    void Melody3()
    {
        SoundPlayer.Instance.PlayMelody3();
    }
}
