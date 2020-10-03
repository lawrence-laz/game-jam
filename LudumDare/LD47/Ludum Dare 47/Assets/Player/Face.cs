using UnityEngine;

public class Face : MonoBehaviour
{
    public GameObject CurrentFace;
    
    [Header("Faces")]
    public GameObject Default;
    public GameObject VeryExcided;
    public GameObject Intense;
    public GameObject Sleep;
    public GameObject GameOver;

    public void ResetFace()
    {
        SetFace(Default);
    }

    public void SetFace(GameObject face)
    {
        if (CurrentFace == face)
        {
            return;
        }

        CurrentFace.SetActive(false);
        CurrentFace = face;
        CurrentFace.SetActive(true);
    }

    private void Start()
    {
        CurrentFace = Default;
    }
}
