using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmearTomatoesInteraction : Interaction
{
    public List<Sprite> Sprites = new();
    public SpriteRenderer WorkbenchSpriteRenderer;

    public override string Text => "Smear";

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        return Sprites.Count > 0
            && interactor.GetComponentInChildren<Holder>().Items.Any(item => item.GetComponent<Label>().Text == "tomatoe");
    }

    public override void Invoke(Interactor interactor, GameObject target)
    {
        if (Sprites.Count == 0)
        {
            return;
        }

        var holder = interactor.GetComponentInChildren<Holder>();
        var tomatoe = holder.Items.FirstOrDefault(item => item.GetComponent<Label>().Text == "tomatoe");
        holder.TryDrop(tomatoe.GetComponent<Pickable>());
        Destroy(tomatoe.gameObject);
        WorkbenchSpriteRenderer.sprite = Sprites.First();
        if (Sprites.Count != 0)
        {
            Sprites.RemoveAt(0);
        }
        else
        {
            Debug.Log("AAAAAAAAAAAAAAAAAA");
        }
    }
}
