using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyFuel : MonoBehaviour
{
    public int PriceFuelSecond = 2;
    private Text _text;
    private GameObject _player;
    private Fuel _fuel;
    private Credits _credits;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SellAllMinerals);
        _text = GetComponentInChildren<Text>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _fuel = _player.GetComponentInChildren<Fuel>();
        _credits = FindObjectOfType<Credits>();
    }

    private void Update()
    {
        _text.text = $"Buy fuel{Environment.NewLine}(-{GetPrice()} €)";
    }

    private void SellAllMinerals()
    {
        var price = GetPrice();
        var fixValue = price / PriceFuelSecond;

        DOTween.Sequence()
            .AppendCallback(() => _button.enabled = false)
            .Append(DOTween.To(() => _credits.Value, x => _credits.Value = x, _credits.Value - GetPrice(), 0.2f))
            .Join(DOTween.To(() => _fuel.CurrentValue, x => _fuel.CurrentValue = x, _fuel.CurrentValue + fixValue, 0.2f))
            .AppendCallback(() => _button.enabled = true)
            .Play();
    }

    private int GetPrice()
    {
        return Mathf.Min((int)(_fuel.MaxValue - _fuel.CurrentValue) * PriceFuelSecond, _credits.Value);
    }
}
