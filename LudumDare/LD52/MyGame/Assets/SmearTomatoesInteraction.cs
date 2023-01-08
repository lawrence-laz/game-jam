using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class SmearTomatoesInteraction : Interaction
{
    public List<Sprite> Sprites = new();
    public SpriteRenderer WorkbenchSpriteRenderer;
    public UnityEvent OnFinish;
    public ProcessRecipe[] AlchemyRecipes;
    public GameObject LightningPrefab;

    public override string Text => "Smear";

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        return Sprites.Count > 0
            && interactor.GetComponentInChildren<Holder>().Items.Any(item => item.GetComponent<Label>().Is("tomatoe"));
    }

    public override Sequence Invoke(Interactor interactor, GameObject target)
    {
        if (Sprites.Count == 0)
        {
            return null;
        }

        var holder = interactor.GetComponentInChildren<Holder>();
        var tomatoe = holder.Items.FirstOrDefault(item => item.GetComponent<Label>().Is("tomatoe"));
        holder.TryDrop(tomatoe.GetComponent<Pickable>());
        Destroy(tomatoe.gameObject);
        WorkbenchSpriteRenderer.sprite = Sprites.First();
        if (Sprites.Count != 0)
        {
            Sprites.RemoveAt(0);
        }

        if (Sprites.Count == 0)
        {
            Instantiate(LightningPrefab);
            OnFinish.Invoke();
            GetComponent<Recipes>().ProcessRecipes = GetComponent<Recipes>()
                .ProcessRecipes
                .Concat(AlchemyRecipes)
                .ToArray();
        }
        return null;
    }
}
