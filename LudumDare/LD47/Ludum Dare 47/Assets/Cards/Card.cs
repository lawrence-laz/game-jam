using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    private readonly Vector3 _graveyardPosition = new Vector3(100, 100, 0);

    public int EnergyCost = 8;
    public int Duration => Mathf.Abs(EnergyCost);

    public TextMesh TimeCostText;

    public UnityEvent OnActivated;
    public UnityEvent OnActivationFinished;
    public List<Func<Tween>> OnActivationStepTween = new List<Func<Tween>>();
    public UnityEventGameObject OnPlaced;

    public void Activate()
    {
        Debug.Log($"Played card {gameObject.name}.");

        var clock = FindObjectOfType<Clock>();
        var stats = FindObjectOfType<Stats>();

        OnActivated.Invoke();

        var sequence = DOTween.Sequence();

        for (int i = 0; i < Duration; i++)
        {
            Debug.Log($"will remove {EnergyCost / Duration} energy.");

            sequence
                .Append(clock.IncreaseHours(1))
                .Join(DOTween.To(() => stats.Energy, x => stats.Energy = x, -EnergyCost / Duration, clock.StepDuration).SetRelative(true));

            var tweens = OnActivationStepTween
                .Select(x => x.Invoke())
                .Where(x => x != null)
                .ToArray();

            if (tweens.Length > 0)
            {
                foreach (var tween in tweens)
                {
                    sequence.Append(tween);
                }
            }
        }

        sequence.Append(transform.DOMove(_graveyardPosition, clock.StepDuration));

        sequence.AppendCallback(() => OnActivationFinished.Invoke());

        sequence.Play();
    }

    public void PlaceOn(GameObject target)
    {
        OnPlaced.Invoke(target);
    }

    private void Start()
    {
        TimeCostText.text = EnergyCost.ToString().Replace('-', '+');
    }
}
