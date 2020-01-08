using UnityEngine;

[CreateAssetMenu(fileName = "Event name", menuName = "EVENT <<<", order = 0)]
public class Event : ScriptableObject
{
    public PostText[] PostsText;
    public PostImage[] PostsImage;
    public Event[] EnablesEvents;
    public CoinChangeValue[] CoinEffects;
    public int Probability; // Weight compared to others.
}

[System.Serializable]
public class CoinChangeValue
{
    public string CoinName;
    public float VelocityChangeAmount; // Turi pasikeisti ne iš karto, o per kažkokį const laiką?
    public float FadeOutSpeed;
}