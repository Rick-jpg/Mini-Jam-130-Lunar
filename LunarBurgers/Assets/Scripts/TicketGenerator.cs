using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TicketGenerator : MonoBehaviour
{
    [SerializeField] private PowerupHUDTimer ticketTimer;
    [SerializeField] private List<OrderButton> buttons = new List<OrderButton>();

    List<Ingredient> ticketIngredients = new List<Ingredient>();

    [SerializeField] Animator ticketAnimator;
    [SerializeField] private Animator tabletAnimator;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private Animator lilguyAnimator;

    private string animationTicketAppear = "TicketAppear";
    private string tabletDisappear = "TabletDisappear";
    private string fade = "FadeBlackIn";
    private string lilguyWave = "Waving";

    private int animationTabletHash;
    private int animationTicketHash;
    private int fadeHash;
    private int animationlilguyHash;

    private void OnEnable()
    {
        OrderManager.OnStartWritingTicket += StartTimer;
        PowerupHUDTimer.OnFinishTimer += CreateTicket;
    }

    private void OnDisable()
    {
        OrderManager.OnStartWritingTicket -= StartTimer;
        PowerupHUDTimer.OnFinishTimer -= CreateTicket;
    }

    private void Start()
    {
        buttons = GetComponentsInChildren<OrderButton>().ToList();
        animationTicketHash = Animator.StringToHash(animationTicketAppear);
        animationTabletHash = Animator.StringToHash(tabletDisappear);
        animationlilguyHash = Animator.StringToHash(lilguyWave);
        fadeHash = Animator.StringToHash(fade);
    }

    void StartTimer()
    {
        ticketTimer.gameObject.SetActive(true);
    }

    private void CreateTicket(PowerupHUDTimer timer)
    {
        StartCoroutine(GenerateTicket());
        GameManager.Instance.GoToGathering();
    }

    IEnumerator GenerateTicket()
    {
        ticketTimer.gameObject.SetActive(false);
        lilguyAnimator.Play(animationlilguyHash);
        tabletAnimator.Play(animationTabletHash);
        foreach (OrderButton orderButton in buttons)
        {
            for (int i = 0; i < orderButton.CurrentOrdered; i++)
            {
                ticketIngredients.Add(orderButton.AssignedIngredient);
            }
        }
        GameManager.Instance.playersTicket = ticketIngredients;
        yield return new WaitForSeconds(.5f);
        fadeAnimator.Play(fadeHash);
        yield return new WaitForSeconds(1.5f);
    }
}
