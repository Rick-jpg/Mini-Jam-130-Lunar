using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public delegate void StartOrder();
    public static event StartOrder OnStartOrder;

    public delegate void StartWritingTicket();
    public static event StartWritingTicket OnStartWritingTicket;

    [Header("Animators")]
    [SerializeField] private Animator speechBubbleAnimator;
    [SerializeField] private Animator iconAnimator;
    [SerializeField] private Animator tabletAnimator;

    private string animationCloudAppear = "SpeakingCloudAppear";
    private string animationCloudDisappear = "SpeakingCloudDisappear";
    private string iconDisappear = "IconDisappear";
    private string tabletAppear = "TabletAppear";

    private int aniamtionCloudHash;
    private int animationCLoudCloseHash;
    private int animationIconHash;
    private int animationTabletHash;

    [SerializeField] private RawImage speechBubbleIcon;

    private void OnEnable()
    {
        Customer.onOrderComplete += StartTellingOrder;
    }

    private void OnDisable()
    {
        Customer.onOrderComplete -= StartTellingOrder;
    }

    // Start is called before the first frame update
    void Start()
    {
        aniamtionCloudHash = Animator.StringToHash(animationCloudAppear);
        animationCLoudCloseHash = Animator.StringToHash(animationCloudDisappear);
        animationIconHash = Animator.StringToHash(iconDisappear);
        animationTabletHash = Animator.StringToHash(tabletAppear);

        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(2f);
        OnStartOrder?.Invoke();
        yield return new WaitForSeconds(1f);
    }

    void StartTellingOrder()
    {
        StartCoroutine(StartTellingOrderSequence());
    }

    IEnumerator StartTellingOrderSequence()
    {
        yield return new WaitForSeconds(1f);
        speechBubbleAnimator.Play(aniamtionCloudHash);
        yield return new WaitForSeconds(1f);

        List<Ingredient> order = GameManager.Instance.customerOrder;
        int times = order.Count;

        for (int i = 0; i < times; i++)
        {
            iconAnimator.Play(animationIconHash);
            yield return new WaitForSeconds(.65f);
            speechBubbleIcon.texture = order[i].ingredientIcon.texture;
            yield return new WaitForSeconds(1.3f);
        }

        speechBubbleAnimator.Play(animationCLoudCloseHash);
        yield return new WaitForSeconds(1.5f);
        tabletAnimator.Play(animationTabletHash);
        yield return new WaitForSeconds(1f);
        OnStartWritingTicket?.Invoke();
    }
}
