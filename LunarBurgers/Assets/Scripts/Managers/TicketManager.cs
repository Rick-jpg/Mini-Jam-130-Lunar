using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketManager : MonoBehaviour
{
    [SerializeField] List<Ingredient> ingredientsOnTicket = new List<Ingredient>();
    [SerializeField] GameObject ticketItemPrefab;
    [SerializeField] GameObject ticketItemPlacement;
    private List<TicketItem> allTickets = new List<TicketItem>();

    private void OnEnable()
    {
        Ingredient.OnCollected += CheckItemOnList;
    }

    private void OnDisable()
    {
        Ingredient.OnCollected -= CheckItemOnList;
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateList();
    }

    void GenerateList()
    {
        ingredientsOnTicket = GameManager.Instance.playersTicket;
        foreach (Ingredient ingredient in ingredientsOnTicket)
        {
            var ticketItem = Instantiate(ticketItemPrefab, ticketItemPrefab.transform.position, Quaternion.identity);
            ticketItem.transform.parent = ticketItemPlacement.transform;

            TicketItem ti = ticketItem.GetComponentInChildren<TicketItem>();
            ti.SetupTicketLine(ingredient);
            allTickets.Add(ti);
        }
    }

    void CheckItemOnList(Ingredient i)
    {
        foreach (TicketItem ti in allTickets)
        {
            if (ti.hasBeenCrossed) continue;
            ti.CheckOffList(i);
            if (ti.hasBeenCrossed) break;
        }
    }
}
