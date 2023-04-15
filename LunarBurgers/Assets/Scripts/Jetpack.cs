using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour, IProcessInputHandler
{
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private PlayerController player;
    private bool isUsingJetpack;
    private bool canUseJetpack;

    [Range(0, 10)]
    private float currentJetpackProgress;
    private float maxJetpackProgress = 2;

    private float jetpackActivationThreshold = 0.7f;
    private float jetpackTime;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        player = GetComponent<PlayerController>();
        currentJetpackProgress = maxJetpackProgress;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        HandleJetpack();
        if (!player.isGrounded()) return;
        RefillProgress();
    }

    public void ProcessInput()
    {
        isUsingJetpack = inputManager.jumpInput;
    }

    void HandleJetpack()
    {
        CheckForAvailabilty();
        if (canUseJetpack && player.isGrounded() == false)
        {
            jetpackTime += Time.deltaTime;
        }
        else
        {
            jetpackTime = 0;
        }

        if(isUsingJetpack && jetpackTime > jetpackActivationThreshold && currentJetpackProgress > 0)
        {
            player.EnableGravity(false);
            if (currentJetpackProgress <= 0) return;
            currentJetpackProgress -= Time.deltaTime;
        }
        else
        {
            if (player.UseGravity) return;
            player.EnableGravity(true);
        }    

    }

    private void CheckForAvailabilty()
    {
        if (isUsingJetpack && currentJetpackProgress < 0)
        {
            canUseJetpack = false;
        }
        else if (!isUsingJetpack && currentJetpackProgress > 0)
        {
            canUseJetpack = true;
        }
    }

    public void FillToMax()
    {
        currentJetpackProgress = maxJetpackProgress;
    }

    void RefillProgress()
    {
        if (currentJetpackProgress >= maxJetpackProgress) return;
        currentJetpackProgress += Time.deltaTime;
    }
}
