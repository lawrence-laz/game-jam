using UnityEngine;
using UnityEngine.Events;
using UnityGoodies;

public class ArrowBehaviour : MonoBehaviour
{
    public bool IsReleased = false;
    public float Speed = 5;
    public LayerMask Targets;
    public bool KillerArrow = false;
    public Transform KillTarget;
    public Transform WhoShot;

    public UnityEvent OnReleased;
    public UnityEvent OnStick;

    public void Release()
    {
        IsReleased = true;
        OnReleased.Invoke();
    }

    private void Update()
    {
        if (KillerArrow && KillTarget)
        {
            transform.LookAt(KillTarget.position + Vector3.down * 0.3f);
        }

        if (IsReleased && Speed > 0)
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Speed == 0)
        {
            return;
        }

        if (!collision.transform.gameObject.IsInLayerFrom(Targets))
        {
            return;
        }

        Stick(collision.transform);
        DamagePlayer(collision.transform);
    }

    private void DamagePlayer(Transform transform)
    {
        var root = transform.GetRoot();
        if (root.tag == "Player")
        {
            Debug.LogFormat("An arrow <color=red>killed</color> {0}", root.gameObject.name);
            root.ForAllComponentsInChildren<HealthBehaviour>(hp =>
                {
                    hp.Health = 0;
                    hp.LastDamager = WhoShot;
                });
        }
    }

    private void Stick(Transform target)
    {
        Speed = 0;
        var body = GetComponent<Rigidbody>();
        if (body)
        {
            Destroy(body);
        }

        transform.SetParent(target);
        OnStick.Invoke();
    }
}
