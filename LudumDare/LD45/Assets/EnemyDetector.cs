using DG.Tweening;
using Libs.Base.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetector : MonoBehaviour
{
    public List<Transform> Enemies;

    public AudioSource AudioSource { get; private set; }

    public UnityEvent OnEnemyNearby;
    public Transform Arrow;
    public Transform Line;
    public SpriteRenderer Screen;

    private bool arrowShow;

    private void Start()
    {
        Enemies = new List<Transform>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Enemies.RemoveAll(x => x == null);
        if (Enemies.Count == 0)
        {
            NoEnemiesDetected();
            return;
        }
        var closest = Enemies.OrderBy(x => x.DistanceTo(transform)).FirstOrDefault();

        if (closest == null)
        {
            NoEnemiesDetected();
        }
        else
        {
            EnemyDetected(closest);
        }
    }

    private void EnemyDetected(Transform enemy)
    {
        if (!arrowShow)
        {
            arrowShow = true;
            Line.gameObject.SetActive(false);

            AudioSource.Play();

            DOTween.Sequence()
                .Append(Screen.DOColor(new Color(0, 0, 0, 0), 0.1f))
                .AppendCallback(() => Arrow.gameObject.SetActive(true))
                .Append(Screen.DOColor(Color.red, 0.1f))
                .Append(Screen.DOColor(new Color(0, 0, 0, 0), 0.1f))
                .Append(Screen.DOColor(Color.red, 0.1f));
        }

        Arrow.transform.LookAt2D(enemy.position);
        Arrow.transform.localRotation *= Quaternion.AngleAxis(transform.GetRoot().localEulerAngles.y, Vector3.forward);
    }

    private void NoEnemiesDetected()
    {
        arrowShow = false;
        Arrow.gameObject.SetActive(false);
        Line.gameObject.SetActive(true);

        Screen.DOColor(Color.green, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.IsInLayerFrom(LayerMask.GetMask("Enemy")))
            return;

        if (Enemies.Contains(other.transform))
            return;

        Enemies.Add(other.transform);

        AudioSource.Play();

        OnEnemyNearby.Invoke();
    }
}
