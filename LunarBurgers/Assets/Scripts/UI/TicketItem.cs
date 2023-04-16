using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TicketItem : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private Ingredient assignedIngredient;
    public bool hasBeenCrossed;

    public void SetupTicketLine(Ingredient AssignedIngredient)
    {
        text = GetComponent<TMP_Text>();
        assignedIngredient = AssignedIngredient;

        text.text = assignedIngredient.ingredientName;
    }


    public void CheckOffList(Ingredient i)
    {
        if (hasBeenCrossed) return;
        if (i.ingredientName != assignedIngredient.ingredientName) return;
        text.fontStyle = FontStyles.Strikethrough;
        hasBeenCrossed = true;
    }
}
