using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SunEffect : EffectBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject sunEffect;
    [SerializeField]
    private float fadeSpeed;
    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 end;

    private Image sunSprite;
    private RectTransform sunTransform;

    public override IEnumerator PreShotEffect(Vector3 position)
    {
        sunEffect.SetActive(true);
        sunSprite.color = Color.white;
        sunTransform.localPosition = start;

        do
        {
            yield return new WaitForFixedUpdate();
        } while (sunTransform.localPosition != end);
    }

    public override IEnumerator PostShotEffect(Vector3 position)
    {
        yield return new WaitForSeconds(duration);
        Coroutine fadeOut = StartCoroutine(FadeOut());
        yield return fadeOut;
        sunEffect.SetActive(false);
    }

    private void OnEnable()
    {
        sunSprite = sunEffect.GetComponent<Image>();
        sunTransform = sunEffect.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        MoveTowardsEnd();
    }

    private void MoveTowardsEnd()
    {
        if (sunTransform.localPosition == end || !sunEffect.activeSelf)
            return;

        sunTransform.localPosition = Vector3.MoveTowards(sunTransform.localPosition, end, speed);
    }

    private IEnumerator FadeOut()
    {
        do
        {
            Color color = sunSprite.color;
            color.a = Mathf.MoveTowards(color.a, 0, fadeSpeed);
            sunSprite.color = color;
            yield return new WaitForEndOfFrame();
        } while (sunSprite.color.a != 0);
    }

    //private void CalculateStartAndEndPositions()
    //{
    //    end = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1f));
    //    end = sunEffect.transform.InverseTransformPoint(end);
    //    end.z = 0;

    //    start = end + Vector3.up * sunSprite.sprite.bounds.size.y;
    //    start = sunEffect.transform.InverseTransformPoint(start);
    //    start.z = 0;
    //}
}
