using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RepairShip : MonoBehaviour
{
    public int PricePerHealth = 5;
    private Text _text;
    private GameObject _player;
    private Health _health;
    private Credits _credits;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SellAllMinerals);
        _text = GetComponentInChildren<Text>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _health = _player.GetComponentInChildren<Health>();
        _credits = FindObjectOfType<Credits>();
    }

    private void Update()
    {
        _text.text = $"Repair ship{Environment.NewLine}(-{GetPrice()} €)";
    }

    private void SellAllMinerals()
    {
        var price = GetPrice();
        var fixValue = price / PricePerHealth;

        DOTween.Sequence()
            .AppendCallback(() => _button.enabled = false)
            .Append(DOTween.To(() => _credits.Value, x => _credits.Value = x, _credits.Value - GetPrice(), 0.2f))
            .Join(DOTween.To(() => _health.CurrentValue, x => _health.CurrentValue = x, _health.CurrentValue + fixValue, 0.2f))
            .AppendCallback(() => _button.enabled = true)
            .Play();
    }

    private int GetPrice()
    {
        return Mathf.Min((int)(_health.MaxValue - _health.CurrentValue) * PricePerHealth, _credits.Value);
    }
}
