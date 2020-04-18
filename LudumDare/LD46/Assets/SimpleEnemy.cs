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
    public GameOver GameOver { get; set; }
    public Death Death { get; set; }

    public Vector3 NextStep { get; set; }
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
        GameOver = FindObjectOfType<GameOver>();
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
            GameOver.Invoke();
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
        if (!IsTriggered && transform.position.x != Hero.transform.position.x && transform.position.y != Hero.transform.position.y)
        {
            return;
        }

        var path = transform.position.LineTo(Hero.transform.position).Skip(1).ToArray();
        if (path.Length == 0)
        {
            return;
        }
        if (path.All(point => Map.IsTraversible(point)))
        {
            NextStep = path[0];
            if (!IsTriggered)
            {
                IsTriggered = true;
                OnTriggered.Invoke(); // TODO: add triggered animation
            }
        }
        else
        {
            IsTriggered = false;
        }
    }

    private void OnTurnStarted()
    {
        if (IsTriggered && Hero.transform.position != transform.position)
        {
            if (Map.GetAll(NextStep).Any(x => x.tag == "Enemy"))
            {
                return;
            }

            Move.MoveTo(NextStep);
        }
    }
}
