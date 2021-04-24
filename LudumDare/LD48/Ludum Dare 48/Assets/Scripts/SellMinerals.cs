using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SellMinerals : MonoBehaviour
{
    public int PricePerMineral = 10;
    private Text _text;
    private GameObject _player;
    private Storage _storage;
    private Credits _credits;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SellAllMinerals);
        _text = GetComponentInChildren<Text>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _storage = _player.GetComponentInChildren<Storage>();
        _credits = FindObjectOfType<Credits>();
    }

    private void Update()
    {
        _text.text = $"Sell minerals{Environment.NewLine}(+{GetProfit()} €)";
    }

    private void SellAllMinerals()
    {
        DOTween.Sequence()
            .AppendCallback(() => _button.enabled = false)
            .Append(DOTween.To(() => _credits.Value, x => _credits.Value = x, _credits.Value + GetProfit(), .2f))
            .Join(DOTween.To(() => _storage.CurrentValue, x => _storage.CurrentValue = x, 0, .2f))
            .AppendCallback(() => _button.enabled = true)
            .Play();
    }

    private int GetProfit()
    {
        return (int)_storage.CurrentValue * PricePerMineral;
    }
}
