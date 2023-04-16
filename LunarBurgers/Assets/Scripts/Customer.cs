using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    List<Ingredient> customerOrders = new List<Ingredient>();

    [SerializeField] private int customerOrderLength;

 
    [SerializeField] private Ingredient[] allExtraIngredients = new Ingredient[6];
    [SerializeField] private int[] maxAmount = {0, 2, 3, 3, 3, 3};

    public delegate void OrderComplete();
    public static event OrderComplete onOrderComplete;

    private Ingredient lastIngredient;
    private int ingredientTotal;

    private void OnEnable()
    {
        OrderManager.OnStartOrder += CreateOrder;
    }

    private void OnDisable()
    {
        OrderManager.OnStartOrder -= CreateOrder;
    }

    void CreateOrder()
    {
        //First the bun
        customerOrders.Add(allExtraIngredients[0]);
        //Secondly some Meat
        customerOrders.Add(allExtraIngredients[1]);
        lastIngredient = allExtraIngredients[1];
        //Then the order gets randomized
        for (int i = 0; i < customerOrderLength; i++)
        {
            Ingredient chosenIngredient = GetRandomIngredient();
            customerOrders.Add(chosenIngredient);
        }

        GameManager.Instance.customerOrder = customerOrders;
        onOrderComplete.Invoke();

    }

    Ingredient GetRandomIngredient()
    {
        int randomNumber = Random.Range(1, 5);
        Ingredient randomIngredient = allExtraIngredients[randomNumber];
        ingredientTotal = maxAmount[randomNumber] - 1;
        for (int i = 0; i < 100; i++)
        {
            if (CanBeAddedToList(ref randomNumber, ref randomIngredient))
                { continue; }

            //If it's going over than we get the next one which is 1.
            if (randomNumber == 5)
            {
                randomNumber = 0;
            }
            randomIngredient = allExtraIngredients[randomNumber + 1];
            ingredientTotal = maxAmount[randomNumber] - 1;
        }
        lastIngredient = randomIngredient;
        maxAmount[randomNumber] -= 1;
        return randomIngredient;
    }

    private bool CanBeAddedToList(ref int randomNumber, ref Ingredient randomIngredient)
    {
        if (lastIngredient == randomIngredient) return false;
        if (ingredientTotal < 0) return false;

        return true;
    }
}
