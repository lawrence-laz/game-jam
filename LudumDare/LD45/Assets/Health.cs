using Libs.Base.Extensions;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int Value = 10;
    public UnityEvent OnDeath;
    public GameObject DeathEffect;

    public CameraShake CameraShake { get; private set; }
    public Score Score { get; private set; }

    private void Start()
    {
        CameraShake = FindObjectOfType<CameraShake>();
        if (gameObject.IsInLayerFrom(LayerMask.GetMask("Enemy")))
        {
            Score = FindObjectOfType<Score>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Value--;
            Thread.Sleep(1000 / 23 / 2);
        }

        if (Value <= 0)
        {
            if (Score != null)
            {
                Score.Current++;
                Score = null;
            }
            OnDeath.Invoke();
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.02f);
            CameraShake.SlightShake();
        }
    }
}
