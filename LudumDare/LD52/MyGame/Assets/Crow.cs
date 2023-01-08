using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrowState
{
    None,
    HuntWorm,
    Flee,
}

public class Crow : MonoBehaviour
{
    public CrowState State = CrowState.None;
    public bool IsHumanNearby;
    public Transform Target;
    public float SafeDistance = 1.1f;

    private SpriteFrameAnimator _animator;
    private Rigidbody2D _body;
    private bool _isCaptive;

    private void Start()
    {
        _animator = GetComponentInChildren<SpriteFrameAnimator>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateAnimationAndScale();

        if (State == CrowState.None)
        {
            ChooseWhatToDoNext();
        }

        Act();

        RandomlyLookAtHero();
        TryEscapeIfHeld();
    }

    private void TryEscapeIfHeld()
    {
        _isCaptive = transform.parent != null && transform.parent.GetComponent<Holder>() != null;
    }

    public void RandomlyLookAtHero()
    {
        if (Random.Range(0, 1f) < 0.005f)
        {
            var hero = FindObjectOfType<Hero>().transform;
            var scale = transform.localScale;
            scale.x = hero.transform.position.x - transform.position.x > 0
                ? 1
                : -1;
            transform.localScale = scale;
        }
    }

    public void Act()
    {
        var hero = FindObjectOfType<Hero>().transform;
        if (State == CrowState.Flee)
        {
            if (hero.DistanceTo2D(transform) > SafeDistance)
            {
                Debug.Log($"stop fleeing, far away ({hero.DistanceTo2D(transform)})");
                GetComponent<Movement>().Direction = Vector2.zero;
                State = CrowState.None;
            }
            else
            {
                Debug.Log($"fleeing! ({hero.DistanceTo2D(transform)})");
                GetComponent<Movement>().Direction = -transform.DirectionTo2D(hero);
            }
        }
        else if (State == CrowState.HuntWorm)
        {
            if (Target == null)
            {
                var worm = FindObjectOfType<Worm>();
                if (worm != null)
                {
                    Target = worm.transform;
                }
            }
            // Debug.Log($"distance to hero {Target.DistanceTo2D(hero)}");
            if (Target != null && Target.DistanceTo2D(hero) < SafeDistance)
            {
                Target = null;
            }

            if (Target != null)
            {
                if (Target.DistanceTo2D(transform) > 0.5f)
                {
                    GetComponent<Movement>().Direction = transform.DirectionTo2D(Target);
                }
                else
                {
                    Destroy(Target.gameObject);
                    Target = null;
                }
            }
            else
            {
                GetComponent<Movement>().Direction = Vector2.zero;
                State = CrowState.None;
            }
        }
    }

    private void UpdateAnimationAndScale()
    {
        if (_body.velocity.magnitude > Mathf.Epsilon || _isCaptive)
        {
            _animator.StartAnimation();
        }
        else
        {
            _animator.StopAnimation();
        }

        var scale = transform.localScale;
        if (_body.velocity.x > 0)
        {
            scale.x = 1;
        }
        else if (_body.velocity.x < 0)
        {
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    private void ChooseWhatToDoNext()
    {
        if (IsHumanNearby)
        {
            Debug.Log("Flee");
            State = CrowState.Flee;
        }
        else
        {
            Debug.Log("hunt");
            State = CrowState.HuntWorm;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.GetComponent<Hero>() ?? other.GetComponentInParent<Hero>() != null)
        {
            IsHumanNearby = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.GetComponent<Hero>() ?? other.GetComponentInParent<Hero>() != null)
        {
            IsHumanNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.GetComponent<Hero>() ?? other.GetComponentInParent<Hero>() != null)
        {
            IsHumanNearby = false;
        }
    }
}
