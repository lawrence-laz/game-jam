using System.Collections.Generic;
using UnityEngine;
using UnityGoodies;

public class AttentionAttractorBehaviour : MonoBehaviour
{
    public float AttractAttentionDistance = 4;

    private List<GameObject> _history;

    private void Start()
    {
        InvokeRepeating("CallNearbyEnemies", 1, 0.5f);
        _history = new List<GameObject>();
    }

    private void CallNearbyEnemies()
    {
        if (!enabled)
            return;

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (_history.Contains(enemy))
            {
                continue;
            }

            var distance = enemy.transform.position.DistanceTo(transform.position);
            if (distance > AttractAttentionDistance)
            {
                Debug.LogFormat("{0} Is too far away to notice {1}", enemy.name, distance);
                continue;
            }

            _history.Add(enemy);
            Debug.LogFormat("{0} <color=yellow>attracted attention</color> of {1}", gameObject.name, enemy.name);

            enemy.transform.ForAllComponentsInChildren<NoticeBehaviour>(
                notice => notice.CheckSound(transform.position)
            );
        }
    }
}
