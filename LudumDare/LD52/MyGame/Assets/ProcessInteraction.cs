using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Libs.Base.Extensions;
using UnityEngine;

[Serializable]
public class ProcessRecipe
{
    public string LabelName;
    public GameObject ResultPrefab;
    public Vector2Int Counts;
}

public class ProcessInteraction : Interaction
{
    public override string Text => "Process {held}";

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        var holder = interactor.GetComponentInChildren<Holder>();
        if (holder == null)
        {
            return false;
        }

        var recipe = FindObjectOfType<Recipes>()
            .ProcessRecipes
            .FirstOrDefault(recipe => holder
                .Items.Any(item => item.GetComponent<Label>()?.Text == recipe.LabelName));
        if (recipe == null)
        {
            return false;
        }

        TextTerm.Set("{held}", recipe.LabelName);

        return true;
    }

    public override void Invoke(Interactor interactor, GameObject target)
    {
        var holder = interactor.GetComponentInChildren<Holder>();
        if (holder == null)
        {
            return;
        }

        var recipe = FindObjectOfType<Recipes>()
            .ProcessRecipes
            .FirstOrDefault(recipe => holder
                .Items.Any(item => item.GetComponent<Label>()?.Text == recipe.LabelName));
        if (recipe == null)
        {
            return;
        }

        var item = holder.Items.First(item => item.GetComponent<Label>().Text == recipe.LabelName);

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
    }
}
