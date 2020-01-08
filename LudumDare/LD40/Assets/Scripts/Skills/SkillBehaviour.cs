using System.Collections;
using UnityEngine;

public class SkillBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float radius = 1;
    [SerializeField]
    protected ButtonCooldownBehaviour cooldown;
    [SerializeField]
    protected EffectBehaviour effect;

    public virtual void OnSelected()
    {
        CursorBehaviour cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorBehaviour>();
        cursor.transform.localScale = Vector3.one * radius;
        cursor.SetImageToCrosshair();
    }

    public void Shoot(Vector3 position)
    {
        StartCoroutine(ShootCoroutine(position));
    }
    
    private IEnumerator ShootCoroutine(Vector3 position)
    {
        cooldown.StartCooldown();
        if (effect != null)
        {
            Coroutine preEffect = StartCoroutine(effect.PreShotEffect(position));
            yield return preEffect;
        }
        ShootLogic(position);
        if (effect != null)
        {
            Coroutine postEffect = StartCoroutine(effect.PostShotEffect(position));
            if (postEffect != null)
                yield return postEffect;
        }
    }

    protected virtual void ShootLogic(Vector3 position)
    {

    }

    public virtual void OnDeselect()
    {
        CursorBehaviour cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorBehaviour>();
        cursor.SetImageToCursor();
        cursor.transform.localScale = Vector3.one;

    }
}
