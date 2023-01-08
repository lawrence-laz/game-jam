using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Libs.Base.Extensions;
using UnityEngine;

[Serializable]
public class ProcessRecipe
{
    public string LabelName;
    public string OtherLabelName;
    public GameObject ResultPrefab;
    public Vector2Int Counts;
    public string CustomVerb;
    public Badges Badge = Badges.None;

    public bool HasIngredients(Holder holder)
    {
        return holder.Items.Any(item => item.GetComponent<Label>()?.Is(LabelName) ?? false)
            && (string.IsNullOrEmpty(OtherLabelName) || holder.Items.Any(item => item.GetComponent<Label>()?.Is(OtherLabelName) ?? false));
    }
}

public class ProcessInteraction : Interaction
{
    public override string Text => "{verb} {held}";

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        var holder = interactor.GetComponentInChildren<Holder>();
        if (holder == null)
        {
            return false;
        }

        var recipe = GetComponent<Recipes>()
            .ProcessRecipes
            .FirstOrDefault(recipe =>
            {
                // Debug.Log($"recipe label: {recipe.LabelName}, items:{string.Join(", ", holder.Items.Select(x => x.GetComponent<Label>().Text))}");
                return recipe.HasIngredients(holder);
            });
        if (recipe == null)
        {
            // Debug.Log("recipe not found");
            return false;
        }

        TextTerm.Set("{held}", recipe.LabelName);
        TextTerm.Set("{verb}", string.IsNullOrEmpty(recipe.CustomVerb) ? "Process" : recipe.CustomVerb);

        return true;
    }

    public override Sequence Invoke(Interactor interactor, GameObject target)
    {
        var holder = interactor.GetComponentInChildren<Holder>();
        if (holder == null)
        {
            return null;
        }

        var recipe = GetComponent<Recipes>()
            .ProcessRecipes
            .FirstOrDefault(recipe => recipe.HasIngredients(holder));
        if (recipe == null)
        {
            return null;
        }

        var item = holder.Items.First(item => item.GetComponent<Label>()?.Is(recipe.LabelName) ?? false);
        var otherItem = holder.Items.FirstOrDefault(item => item.GetComponent<Label>()?.Is(recipe.OtherLabelName) ?? false);

        var count = UnityEngine.Random.Range(recipe.Counts.x, recipe.Counts.y + 1);
        for (var i = 0; i < count; ++i)
        {
            var result = Instantiate(recipe.ResultPrefab);
            var positionOffset = interactor.transform.DirectionTo(transform) * 0.5f + new Vector3(0, 0.05f, -0.1f) * i;
            result.transform.position = transform.position + positionOffset;
            result.EnableAllComponentsInChildren<Collider2D>();
        }

        holder.TryDrop(item.GetComponent<Pickable>());
        Destroy(item);

        if (otherItem != null)
        {
            holder.TryDrop(otherItem.GetComponent<Pickable>());
            Destroy(otherItem);
        }

        Badge.Set(recipe.Badge);

        return null;
    }
}
