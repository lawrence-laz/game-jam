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

    public Vector3 NextStep { get; set; }
    public Transform Target { get; set; }
    public bool IsTriggered { get; set; }

    public UnityEvent OnTriggered;

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
    }

    private void OnStepped(GameObject[] steppedBy)
    {
        if (steppedBy.Any(x => x.tag == "Player"))
        {
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

    private void Update()
    {
        //var path = transform.position.LineTo(Hero.transform.position);
        //for (int i = 1; i < path.Length; i++)
        //{
        //    Debug.DrawLine(path[i - 1], path[i]);
        //}
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

            IsTriggered = true;
            Target = target;
            OnTriggered.Invoke(); // TODO: add triggered animation

            NextStep = path[0];
        }
        return;
    }

    public bool TryTrigger(Transform target, out Vector2[] path)
    {
        if (transform.position.x != target.position.x && transform.position.y != target.position.y)
        {
            path = null;
            return false;
        }

        return TryGetPath(target, out path);
    }

    public bool TryGetPath(Transform target, out Vector2[] path)
    {
        path = transform.position.LineTo(target.position).Skip(1).ToArray();
        if (path.Length == 0)
        {
            path = null;
            return false;
        }

        if (path.All(point => Map.IsTraversible(point)))
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
        if (IsTriggered && Hero.transform.position != transform.position)
        {
            if (Map.GetAll(NextStep).Any(x => x.tag == "Enemy" && x.GetComponent<BombEnemy>() == null))
            {
                return;
            }

            Move.MoveTo(NextStep);
        }
    }
}
