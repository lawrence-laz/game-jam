using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    public bool IsLit;
    public ProcessRecipe[] FurnaceRecipes;

    public void OnLit()
    {
        GetComponent<Recipes>().ProcessRecipes = FurnaceRecipes;
    }
}
