using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHudManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject itemDisplayHudGameObject;
    [SerializeField] GameObject timerRestingSpot;
    [SerializeField] PowerupHUDTimer speedBoostTimer;
    [SerializeField] PowerupHUDTimer ItemRevealTimer;

    public delegate void SpeedPickupEnd();
    public static event SpeedPickupEnd OnSpeedPickupEnd;
    public delegate void RevealPickupEnd();
    public static event RevealPickupEnd OnRevealPickupEnd;

    private void OnEnable()
    {
        PowerupHUDTimer.OnFinishTimer += FinishTimer;
        SpeedPowerup.OnPickup += AddSpeedBoostTimer;
        RevealPowerup.OnPickup += AddItemRevealTimer;
    }

    private void OnDisable()
    {
        PowerupHUDTimer.OnFinishTimer -= FinishTimer;
        SpeedPowerup.OnPickup -= AddSpeedBoostTimer;
        RevealPowerup.OnPickup -= AddItemRevealTimer;
    }
    public void AddSpeedBoostTimer()
    {
        if(speedBoostTimer.gameObject.activeInHierarchy)
        {
            speedBoostTimer.ResetToMax();
        }
        else
        {
            speedBoostTimer.gameObject.SetActive(true);
            speedBoostTimer.gameObject.transform.parent = itemDisplayHudGameObject.transform;
        }
    }

    public void AddItemRevealTimer()
    {
        if (ItemRevealTimer.gameObject.activeInHierarchy)
        {
            ItemRevealTimer.ResetToMax();
        }
        else
        {
            ItemRevealTimer.gameObject.SetActive(true);
            ItemRevealTimer.gameObject.transform.parent = itemDisplayHudGameObject.transform;
        }
    }

    public void FinishTimer(PowerupHUDTimer timer)
    {
        timer.gameObject.transform.parent = timerRestingSpot.transform;
        timer.gameObject.SetActive(false);
        
        if(timer == speedBoostTimer)
        {
            OnSpeedPickupEnd.Invoke();
        }
        else
        {
            OnRevealPickupEnd.Invoke();
        }
    }
}
