using UnityEngine;
using UnityEngine.Events;

public class ArrowShoot : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public GameObject AutoAimArrowPrefab;
    public UnityEvent OnStretch;
    public UnityEvent OnShoot;
    public float MinArrowStretchTime = 0.5f;

    private float _downTime = 0;
    private HeroStats _stats;
    private bool _bufferedArrow = false;

    private void OnEnable()
    {
        _stats = GetComponentInParent<HeroStats>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_bufferedArrow)
        {
            OnStretch.Invoke();
            _downTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0) || _bufferedArrow)
        {
            if (Time.time - _downTime < MinArrowStretchTime)
            {
                _bufferedArrow = true;
                return;
            }

            _bufferedArrow = false;
            OnShoot.Invoke();
            RepeatedAudio.Get("ArrowShot_Audio").PlayTimes++;

            var arrow = Instantiate(
                _stats.AutoAim? AutoAimArrowPrefab : ArrowPrefab, 
                transform.position, 
                transform.rotation);
            var potential = Mathf.Min(1, ((Time.time - _downTime) / _stats.ArrowStretchTime));
            arrow.GetComponent<ArrowFly>().Speed = _stats.ArrowMaxSpeed * Mathf.Max(0.6f, potential);
            var damage = Mathf.RoundToInt(_stats.ArrowDamage * potential);
            Debug.Log("Arrow Potential Damage: " + damage + " (" + _stats.ArrowDamage * potential + ")");
            arrow.GetComponent<ArrowDamage>().Damage = damage;

            if (_stats.MultiShot)
            {
                var arrowCopy1 = Instantiate(arrow);
                arrowCopy1.GetComponent<ArrowFly>().Speed *= 0.8f;
                arrowCopy1.transform.Rotate(Vector3.forward, 15);
                if (Random.Range(0, _stats.BounceArrows ? 3 : 2) == 1)
                    arrowCopy1.GetComponent<ArrowDamage>().Damage = 0;
                var arrowCopy2 = Instantiate(arrow);
                arrowCopy2.GetComponent<ArrowFly>().Speed *= 0.8f;
                arrowCopy2.transform.Rotate(Vector3.forward, -15);
                if (Random.Range(0, _stats.BounceArrows ? 3 : 2) == 1)
                    arrowCopy2.GetComponent<ArrowDamage>().Damage = 0;
            }

            _downTime = Time.time;
        }
    }
}
