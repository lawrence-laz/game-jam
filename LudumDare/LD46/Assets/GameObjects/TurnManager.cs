using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public GameObject Player { get; set; }
    public Move MovePlayer { get; set; }
    public bool TurnInProgress { get; set; }
    public float TurnStarted { get; set; }
    public float TurnDuration { get; set; } = .1f;
    public float HoldDelay { get; set; }

    public UnityEvent OnTurnStarted;
    public UnityEvent OnTurnEnded;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MovePlayer = Player.GetComponent<Move>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            HoldDelay = 0;
        }

        if (TurnInProgress)
        {
            if (Time.time >= TurnStarted + TurnDuration + HoldDelay)
            {
                TurnInProgress = false;
            }
            else
            {
                return;
            }
        }

        var moveInput = GetMoveInput();
        if (moveInput != Vector2.zero && MovePlayer.CanMove(moveInput))
        {
            MovePlayer.MoveBy(moveInput);
            MakeTurn();
            OnTurnEnded.Invoke();
            foreach (var tileObject in FindObjectsOfType<TileObject>())
            {
                tileObject.OnTurnEnded();
            }
        }

        HoldDelay = 0;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Make first step longer.
            HoldDelay = .3f;
        }
    }

    public Vector2 GetMoveInput()
    {
        var offset = Vector3.zero;

        if (Input.GetKey(KeyCode.DownArrow)) offset = Vector3.down;
        else if (Input.GetKey(KeyCode.UpArrow)) offset = Vector3.up;
        else if (Input.GetKey(KeyCode.LeftArrow)) offset = Vector3.left;
        else if (Input.GetKey(KeyCode.RightArrow)) offset = Vector3.right;

        return offset;
    }

    public void MakeTurn()
    {
        if (!TurnInProgress)
        {
            TurnInProgress = true;
        }
        TurnStarted = Time.time;
        OnTurnStarted.Invoke();
    }
}
