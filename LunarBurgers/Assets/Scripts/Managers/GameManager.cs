using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public List<Ingredient> customerOrder = new List<Ingredient>();
    public List<Ingredient> playersTicket = new List<Ingredient>();
    public List<Ingredient> collectedItems = new List<Ingredient>();

    public static GameManager Instance;

    

    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public int CompareListsForCorrection()
    {
        int maxCustomerOrder = customerOrder.Count;
        List<Ingredient> retrievedCorrectItems = (List<Ingredient>)customerOrder.Intersect(collectedItems);
        int missedItemsFromOrder = maxCustomerOrder - retrievedCorrectItems.Count;
        int extraItemsCollected = collectedItems.Count - maxCustomerOrder;
        if (extraItemsCollected < 0) extraItemsCollected = 0;

        return ((maxCustomerOrder - missedItemsFromOrder) - extraItemsCollected);
    }

    public void GoToGathering()
    {
       StartCoroutine(LoadToScene(2));
    }

    IEnumerator LoadToScene(int index)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(index);
    }

    public void GoToReviewing()
    {
        StartCoroutine(LoadToScene(3));
    }
    public void GoToMainMenu()
    {
        StartCoroutine(LoadToScene(0));
    }
    public void GoToOrdering()
    {
        StartCoroutine(LoadToScene(1));
    }

}

