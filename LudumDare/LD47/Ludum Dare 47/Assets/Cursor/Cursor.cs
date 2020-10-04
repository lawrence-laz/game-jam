using DG.Tweening;
using System.Linq;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject DraggingObject;

    public Tooltip Tooltip { get; private set; }

    private void Start()
    {
        Tooltip = FindObjectOfType<Tooltip>();
    }

    public void Update()
    {
        TryGetGameObjectUnderCursor(out var target);

        if (target != null)
        {
            if (target.TryGetComponent(out Card card))
            {
                Tooltip.StandardNote = card.Description;

                var hoverAnimation = card.GetComponentInParent<CardHoverAnimation>();
                if (hoverAnimation != null)
                {
                    hoverAnimation.IsHovered = true;
                }
            }
            else
            {
                Tooltip.StandardNote = null;
            }
        }

        if (Card.Animation != null && Card.Animation.IsActive() && !Card.Animation.IsComplete())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && target != null)
        {
            if (target.CompareTag("Card"))
            {
                DraggingObject = target;
            }
            else if (target.TryGetComponent(out Deck deck))
            {
                //Card.Animation = deck.DrawCard();
                Debug.Log("Drawing from deck manually is disabled");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (DraggingObject != null && DraggingObject.TryGetComponent(out Card card) /*&& TryGetGameObjectUnderCursor(out target, ~LayerMask.GetMask("Card"))*/)
            {
                card.PlaceOn(default);
            }

            DraggingObject = default;
        }
        else if (Input.GetMouseButtonUp(1) && target != null)
        {
            if (target.TryGetComponent(out Card card))
            {
                FindObjectOfType<Hand>().RemoveFromHand(card.transform);
                var stats = FindObjectOfType<Stats>();
                Card.Animation = DOTween.Sequence()
                    .Append(card.transform.DOMove(Vector3.down * 2, 0.3f).SetRelative(true).SetEase(Ease.InCubic))
                    .Join(card.transform.DORotate(Vector3.forward * 450, 0.4f, RotateMode.LocalAxisAdd).SetRelative(true).SetEase(Ease.OutCubic))
                    .Append(FindObjectOfType<Deck>().DrawCard())
                    .AppendCallback(() => FindObjectOfType<Tooltip>().ImportantNote = "Skipping cards makes me <color=red>bored</color>.")
                    .Append(DOTween.To(() => stats.Fun, x => stats.Fun = x, -1f / 4, 0.2f).SetRelative(true))
                    .Play();
            }
        }

        if (DraggingObject)
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                 Input.mousePosition.x,
                 Input.mousePosition.y,
                 DraggingObject.transform.position.z - Camera.main.transform.position.z));

            //Debug.Log(Input.mousePosition);
            var newPosition = new Vector3(
                mouseWorldPosition.x,
                mouseWorldPosition.y,
                DraggingObject.transform.position.z);
            DraggingObject.transform.position = newPosition;
        }
    }

    public static bool TryGetGameObjectUnderCursor(out GameObject gameObject, int layerMask = ~0)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 50, layerMask))
        {
            gameObject = hit.collider.gameObject;
            return true;
        }

        gameObject = default;
        return false;
    }

    public static bool TryGetGameObjectsUnderCursor(out GameObject[] gameObject)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        if (hits?.Length > 0)
        {
            gameObject = hits.Select(x => x.collider.gameObject).ToArray();
            return true;
        }

        gameObject = default;
        return false;
    }
}
