using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerupHUDTimer : MonoBehaviour
{
    public delegate void FinishTimer(PowerupHUDTimer timer);
    public static event FinishTimer OnFinishTimer;
    float maxTime = 10f;
    float currentTime;

    [Header("UI settings")]
    private int timer;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text number;
    // Start is called before the first frame update
    void OnEnable()
    {
        slider.maxValue = maxTime;
        ResetToMax();
    }

    // Update is called once per frame
    void Update()
    {
        if( currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        if (currentTime <= 0)
        {
            OnFinishTimer.Invoke(this);
        }
        ChangeUI(currentTime);
    }

    public void ResetToMax()
    {
        currentTime = maxTime;
        ChangeUI(currentTime);
    }

    public void ChangeUI(float value)
    {
        slider.value = value;
        timer = (int)value;
        number.text = timer.ToString();
    }
}
