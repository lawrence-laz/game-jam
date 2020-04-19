using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SimpleEnemy : MonoBehaviour
{
    public Move Move { get; set; }
    public TurnManager TurnManager { get; set; }
    public Hero Hero { get; set; }
    public Map Map { get; set; }
    public TileObject TileObject { get; set; }
    public Death Death { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    public Vector3 NextStep { get; set; }
    public Transform Target { get; set; }
    public bool IsTriggered { get; set; }

    public Sprite[] AnimationSprites;
    public UnityEvent OnTriggered;
    public GameObject TriggerEffectPrefab;
    public UnityEvent OnAttacked;

    private Sequence _animation;

    private void Start()
    {
        Move = GetComponent<Move>();
        TurnManager = FindObjectOfType<TurnManager>();
        TurnManager.OnTurnStarted.AddListener(OnTurnStarted);
        TurnManager.OnTurnEnded.AddListener(OnTurnEnded);
        Hero = FindObjectOfType<Hero>();
        Map = FindObjectOfType<Map>();
        TileObject = GetComponent<TileObject>();
        TileObject.OnStepped.AddListener(OnStepped);
        Death = GetComponent<Death>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _animation = DOTween.Sequence()
            .AppendCallback(() => SpriteRenderer.sprite = AnimationSprites[0])
            .AppendInterval(0.1f)
            .AppendCallback(() => SpriteRenderer.sprite = AnimationSprites[1])
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        if (_animation != null)
        {
            _animation.Kill();
        }
    }

    private void OnStepped(GameObject[] steppedBy)
    {
        if (steppedBy.Any(x => x.tag == "Player" || x.GetComponent<Box>() != null))
        {
            steppedBy.FirstOrDefault(x => x.tag == "Player")?.GetComponent<AudioSource>()?.Play();
            Death.Die();
        }
        else if (steppedBy.Any(x => x.GetComponent<Hostage>() != null))
        {
            foreach (var hostage in steppedBy.GetComponents<Hostage>())
            {
                hostage.GetComponent<Death>().Die();
            }
        }
    }

    private void OnTurnEnded()
    {
        if (!IsTriggered)
        {
            // Looking for target
            LookForTarget();
            return;
        }
        else if (TryGetPath(Target, out var path))
        {
            // Trying to follow target
            NextStep = path[0];
        }
        else
        {
            // Lost target
            IsTriggered = false;
            Target = null;
            LookForTarget();
        }
    }

    private void LookForTarget()
    {
        var targets = new List<Transform>() { Hero.transform };
        targets.AddRange(FindObjectsOfType<Hostage>().Select(x => x.transform));
        foreach (var target in targets)
        {
            if (!TryTrigger(target, out var path))
            {
                continue;
            }

            Move.UpdateSpriteDirection(target.position - transform.position);

            IsTriggered = true;
            Target = target;
            OnTriggered.Invoke(); // TODO: add triggered animation
            Instantiate(TriggerEffectPrefab, transform.position, Quaternion.identity);

            NextStep = path[0];
        }
        return;
    }

    public bool TryTrigger(Transform target, out Vector2[] path)
    {
        var targetTile = target.GetComponent<TileObject>();

        if (TileObject.Position.x != targetTile.Position.x && TileObject.Position.y != targetTile.Position.y)
        {
            path = null;
            return false;
        }

        return TryGetPath(target, out path);
    }

    public bool TryGetPath(Transform target, out Vector2[] path)
    {
        var targetTile = target.GetComponent<TileObject>();
        path = TileObject.Position.LineTo(targetTile.Position).Skip(1).ToArray();
        if (path.Length == 0)
        {
            path = null;
            return false;
        }

        if (path.All(point => Map.IsTraversible(point, gameObject)))
        {
            return true;
        }
        else
        {
            path = null;
            return false;
        }
    }

    private void OnTurnStarted()
    {
        if (IsTriggered && Hero.TileObject.Position != TileObject.Position)
        {
            if (Map.GetAll(NextStep).Any(x => x.tag == "Enemy" && x.GetComponent<BombEnemy>() == null))
            {
                return;
            }

            Move.MoveTo(NextStep);
        }
    }
}
