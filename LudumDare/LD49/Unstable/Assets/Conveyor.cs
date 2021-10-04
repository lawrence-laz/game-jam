using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float Speed;
    public Transform Player;
    public Vector3 DirectionModifier = Vector3.one;
    public bool OverrideDirection;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Speed *= FindObjectOfType<SpeedManager>().Modifier;
    }

    private void FixedUpdate()
    {
        transform.Translate((OverrideDirection ? DirectionModifier : -Vector3.forward) * Speed * Time.fixedDeltaTime, Space.World);
    }
}
