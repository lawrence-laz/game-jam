using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    public readonly Vector3 ActiveCardPosition = new Vector3(-0.065f, 0.702f, -7.453f);
    public readonly Quaternion ActiveCardRotation = Quaternion.Euler(30f, 1.871f, 0f);
    private readonly Vector3 _graveyardPosition = new Vector3(10, 10, 0);

    public int EnergyCost = 8;
    public int Duration => Mathf.Abs(EnergyCost);

    public TextMesh TimeCostText;

    public UnityEvent OnActivated;
    public UnityEvent OnActivationFinished;
    public GameObject[] OnActivatedEffects;
    public List<Func<Tween>> OnActivationStepTween = new List<Func<Tween>>();
    public UnityEventGameObject OnPlaced;
    public List<Tween> BeforeActivationTween = new List<Tween>();

    private List<GameObject> _effects = new List<GameObject>();

    public void Activate()
    {
        Debug.Log($"Played card {gameObject.name}.");

        var clock = FindObjectOfType<Clock>();
        var stats = FindObjectOfType<Stats>();

        OnActivated.Invoke();

        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(ActiveCardPosition, 0.5f))
            .Join(transform.DORotateQuaternion(ActiveCardRotation, 0.5f))
            .Join(FindObjectOfType<Hand>().HideHand());

        foreach (var tween in BeforeActivationTween)
        {
            sequence.Append(tween);
        }

        if (OnActivatedEffects.Length > 0)
        {
            sequence.AppendCallback(() =>
            {
                foreach (var effect in OnActivatedEffects)
                {
                    _effects.Add(Instantiate(effect, GameObject.FindWithTag("Player").transform));
                }
            });
        }

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

        sequence.AppendCallback(() =>
        {
            foreach (var effect in _effects)
            {
                Destroy(effect);
            }
            _effects.Clear();
        });

        sequence
            .Append(transform.DOMove(_graveyardPosition, 0.5f))
            .AppendCallback(() =>
            {
                FindObjectOfType<Hand>().RemoveFromHand(transform);
                OnActivationFinished.Invoke();
            })
            .Join(FindObjectOfType<Hand>().ShowHand());

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
