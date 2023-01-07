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
}

public class ProcessInteraction : Interaction
{
    public ProcessRecipe[] ProcessRecipes;

    public override string Text => "Process {held}";

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        var holder = interactor.GetComponentInChildren<Holder>();
        if (holder == null)
        {
            return false;
        }

        var recipe = ProcessRecipes
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

        var recipe = ProcessRecipes
            .FirstOrDefault(recipe => holder
                .Items.Any(item => item.GetComponent<Label>()?.Text == recipe.LabelName));
        if (recipe == null)
        {
            return;
        }

        var item = holder.Items.First(item => item.GetComponent<Label>().Text == recipe.LabelName);

        holder.TryDrop(item.GetComponent<Pickable>());
        Destroy(item);
        var result = Instantiate(recipe.ResultPrefab);
        result.transform.position = transform.position;
        result.EnableAllComponentsInChildren<Collider2D>();
    }
}
