using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float[] starRatings = { 1f, 1.5f, 2f, 2.5f, 3, 3.5f, 4, 4.5f, 5f };
    private int[] totalScore = {-1 ,0, 1, 2, 3, 4, 5, 6, 7 };
    private string[] lilguyCommentary = { "Bogos Binted." };

    List<Ingredient> collectedItems;
    List<Ingredient> customerItems;

    private int forgottenItems;
    private int extraItems;

    private float endStarRating;
    private int totalEndScore;
    private string endCommentary;
    private int position;

    [Header("Review Properties")]
    [SerializeField] TMP_Text endCommentaryText;
    [SerializeField] TMP_Text forgottenText, extraText, starRatingText;
    // Start is called before the first frame update
    void Start()
    {
        collectedItems = GameManager.Instance.collectedItems;
        customerItems = GameManager.Instance.customerOrder;

        CheckForRating();
    }

    int CalculateScore()
    {
        List<Ingredient> copy = collectedItems;

        foreach (Ingredient i in customerItems)
        {
            if(copy.Contains(i))
            {
                //Item is checked and did good
                copy.Remove(i);
            }
            else
            {
                forgottenItems++;
            }
        }

        extraItems = copy.Count;

        int maxItems = customerItems.Count;
        int score = ((maxItems - forgottenItems) - extraItems);
        if (score < -1) score = -1;
        return score;
    }

    void CheckForRating()
    {
        totalEndScore = CalculateScore();
        foreach(int i in totalScore)
        {
            if (totalEndScore != totalScore[i]) return;
            position = i;
        }

        endStarRating = starRatings[position];
        endCommentary = lilguyCommentary[position];

        UpdateReview();

    }

    void UpdateReview()
    {
        //Simple
        endCommentaryText.text = endCommentary;

        //Requires more typing more
        starRatingText.text = $"{endStarRating} out of 5 stars!";
        forgottenText.text = $"I ordered a simple Lunar burger with some extras. When I got my burger, I saw that there were <b>{forgottenItems}</b> missing.";
        extraText.text = $"It also appear that the Chef <b>{extraItems}</b> extra ingredients for my burger.";
    }
}
