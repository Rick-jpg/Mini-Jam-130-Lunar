using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrderButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Ingredient assignedIngredient;
    [SerializeField] private int maxOrdered;
    private int currentOrdered;

    [Header("Button Settings")]
    [SerializeField] RawImage baseImage;
    [SerializeField] RawImage icon;
    [SerializeField] TMP_Text amountText;

    public Ingredient AssignedIngredient { get { return assignedIngredient; } set { } }
    public int CurrentOrdered { get { return currentOrdered; } set { } }
    private void Start()
    {
        icon.texture = assignedIngredient.ingredientIcon.texture;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            AddItem();
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            RemoveItem();
        }
    }

    private void RemoveItem()
    {
        currentOrdered--;
        if (currentOrdered < 0)
        {
            currentOrdered = 0;
        }
        ChangeStatusButton();
    }

    private void AddItem()
    {
        currentOrdered++;
        if(currentOrdered > maxOrdered)
        {
            currentOrdered = maxOrdered;
        }
        ChangeStatusButton();
    }

    void ChangeStatusButton()
    {
        if(currentOrdered == maxOrdered)
        {
            baseImage.color = Color.red;
        }
        else
        {
            baseImage.color = Color.white;
        }
        amountText.text = currentOrdered.ToString();
    }
}
