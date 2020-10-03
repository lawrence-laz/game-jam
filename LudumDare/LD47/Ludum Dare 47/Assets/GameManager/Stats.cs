using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public const int MaxEnergyPoints = 24 - 8;
    public const int SecondsToNotFun = 45;
    public const int SecondsToStarving = 65;
    public const float HungerPerHour = 1f / 24 / 2;

    [SerializeField]
    private int _energy = MaxEnergyPoints;
    [SerializeField]
    private float _fun = 1;
    [SerializeField]
    private float _hunger = 1;
    [SerializeField]
    private float _stress = 1;

    public int Energy
    {
        get => _energy;
        set
        {
            _energy = Mathf.Clamp(value, 0, MaxEnergyPoints);
        }
    }

    public float Fun
    {
        get => _fun;
        set
        {
            _fun = Mathf.Clamp01(value);
        }
    }

    public float Hunger
    {
        get => _hunger;
        set
        {
            _hunger = Mathf.Clamp01(value);
        }
    }

    public float Stress
    {
        get => _stress;
        set
        {
            _stress = Mathf.Clamp01(value);
        }
    }

    public Text EnergyValueText;
    public Slider EnergySlider;
    public Slider FunSlider;
    public Slider HungerSlider;
    public Slider StressSlider;

    private void Update()
    {
        //Fun -= Time.deltaTime / SecondsToNotFun;
        //Hunger -= Time.deltaTime / SecondsToStarving;

        UpdateSliders();
    }

    private void UpdateSliders()
    {
        EnergyValueText.text = Mathf.CeilToInt(_energy).ToString();
        EnergySlider.value = (float)_energy / MaxEnergyPoints;
        FunSlider.value = _fun;
        HungerSlider.value = _hunger;
        StressSlider.value = _stress;
    }
}
