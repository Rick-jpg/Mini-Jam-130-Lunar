using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IProcessInputHandler
{
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private CharacterController controller;

    private Vector2 lookInput;
    private Vector2 moveInput;
    private bool isJumping;

    [Header("Movement properties")]
    [SerializeField] private float startSpeed = 5f;
    [SerializeField] private float currentMovementSpeed = 0;
    [SerializeField] private float accelerationRate = 1f;
    [SerializeField] private float deaccelerationRate = 10f;
    [SerializeField] private float maxAcceleration = 7f;
    private Vector3 movement;
    private Vector3 savedVelocity;
    private float ySpeed;

    [Header("Jumping and Gravity")]
    [SerializeField] private float jumpStrength;
    [SerializeField] private const float GRAVITY = -1.62f;
    private float currentGravity;

    [SerializeField] private float jumpPressedTime = 0.5f;
    [SerializeField] private float groundRememberTime = 0.5f;
    private float jumpPressed;
    private float groundRemember;
    private bool hasPressedJump;

    bool useGravity;
    public bool UseGravity { get { return useGravity; } private set { } }
    public Vector3 Movement { get { return movement; } private set { } }

    [Header("Power up")]
    [SerializeField] private float speedModifier = 1f;

    private void OnEnable()
    {
        PowerupHudManager.OnSpeedPickupEnd += DisableModifier;
    }

    private void OnDisable()
    {
        PowerupHudManager.OnSpeedPickupEnd -= DisableModifier;
    }
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        controller = GetComponent<CharacterController>();
        EnableGravity(true);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        if (useGravity)
        {
            ApplyGravity();
        }
        HandleJumping();
        HandleMovement();
    }

    public bool isGrounded()
    {
        return controller.isGrounded;
    }
    public void EnableGravity(bool enable)
    {
        useGravity = enable;

        if(enable)
        {
            currentGravity = GRAVITY;
            ySpeed = currentGravity;
        }
        else
        {
            currentGravity = 0;
            ySpeed = 0;
        }
    }

    private void ApplyGravity()
    {
        groundRemember -= Time.deltaTime;
        if (isGrounded())
        {
            hasPressedJump = false;
            groundRemember = groundRememberTime;
            ySpeed = 0;
        }
        if (ySpeed < 0)
        {
            ySpeed = currentGravity;
        }
        ySpeed += currentGravity * Time.deltaTime;
    }
    private void HandleJumping()
    {
        jumpPressed -= Time.deltaTime;

        if(isJumping && !hasPressedJump)
        {
            hasPressedJump = true;
            jumpPressed = jumpPressedTime;
            ySpeed = Mathf.Sqrt(jumpStrength * -2f * currentGravity);
        }

        if((jumpPressed > 0) && (groundRemember > 0))
        {
            groundRemember = 0;
            jumpPressed = 0;
            ySpeed = Mathf.Sqrt(jumpStrength * -2f * currentGravity);
        }
    }

    private void HandleMovement()
    {
        moveInput.Normalize();
        Vector3 inputMovement = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (inputMovement.magnitude == 0)
        {
            currentMovementSpeed -= deaccelerationRate * Time.deltaTime;
            currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, 0, maxAcceleration);

        }
        else
        {
            currentMovementSpeed += accelerationRate * speedModifier * Time.deltaTime;
            currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, startSpeed, maxAcceleration);
            savedVelocity = inputMovement;
        }


        movement = savedVelocity * currentMovementSpeed + Vector3.up * ySpeed;

        controller.Move(movement * Time.deltaTime);
    }

    public void ProcessInput()
    {
        lookInput = inputManager.lookInput;
        moveInput = inputManager.moveInput;
        isJumping = inputManager.jumpInput;
    }

    #region
    public void EnableModifier()
    {
        speedModifier = 1.5f;
        maxAcceleration = 8f;
    }

    public void DisableModifier()
    {
        speedModifier = 1f;
        maxAcceleration = 6f;
    }
    #endregion

}
