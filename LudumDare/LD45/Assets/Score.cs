using UnityEngine;

public class Score : MonoBehaviour
{
    public int Current = 0;
    public int Best
    {
        get { return PlayerPrefs.GetInt("BEST"); }
        set { PlayerPrefs.SetInt("BEST", value); }
    }
}
