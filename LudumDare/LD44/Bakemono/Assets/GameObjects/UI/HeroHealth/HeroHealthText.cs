using UnityEngine;
using UnityEngine.UI;

public class HeroHealthText : MonoBehaviour
{
    private HeroStats _stats;
    private Text _text;

    private void OnEnable()
    {
        _stats = GameObject.FindWithTag("Player").GetComponent<HeroStats>();
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text = _stats.Health.ToString();
    }
}
