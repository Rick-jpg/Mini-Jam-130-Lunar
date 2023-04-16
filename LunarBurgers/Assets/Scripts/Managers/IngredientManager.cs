using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private List<Ingredient> ingredients = new List<Ingredient>();

    private void OnEnable()
    {
        RevealPowerup.OnPickup += RevealIngredients;
        PowerupHudManager.OnRevealPickupEnd += HideIngredients;
    }

    private void OnDisable()
    {
        RevealPowerup.OnPickup -= RevealIngredients;
        PowerupHudManager.OnRevealPickupEnd -= HideIngredients;

    }

    private void RevealIngredients()
    {
        foreach (Ingredient i in ingredients)
        {
            i.Reveal();
        }
    }

    void HideIngredients()
    {
        foreach (Ingredient i in ingredients)
        {
            i.Hide();
        }
    }

    private void Start()
    {
        ingredients = FindObjectsOfType<Ingredient>().ToList();
    }

    
}
