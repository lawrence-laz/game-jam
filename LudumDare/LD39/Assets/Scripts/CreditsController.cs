using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField]
    GameObject text1;
    [SerializeField]
    GameObject text2;
    [SerializeField]
    GameObject text3;

    private void Start()
    {
        Invoke("_1", 6.7f);
    }

    void _1()
    {
        text1.SetActive(false);
        text2.SetActive(true);
        Invoke("_2", 12.9f - 6.7f);
    }

    void _2()
    {
        text2.SetActive(false);
        text3.SetActive(true);
    }
}
