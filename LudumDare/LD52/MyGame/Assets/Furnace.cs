using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Furnace : MonoBehaviour
{
    public bool IsLit;
    public ProcessRecipe[] FurnaceRecipes;
    public UnityEvent OnLitHandler;

    public void OnLit()
    {
        GetComponent<Recipes>().ProcessRecipes = FurnaceRecipes;
        OnLitHandler.Invoke();
    }
}
