using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool jumpInput;

    private Vector2 playerInputMove;
    private Vector2 playerInputLook;
    private bool playerInputJump;

    private void Update()
    {
        ProcessMoveInput();
        ProcessLookInput();
        ProcessJumpInput();
    }
    public void ProcessLookInput()
    {
        lookInput = playerInputLook;
    }

    public void ProcessMoveInput()
    {
        moveInput = playerInputMove;
    }

    public void ProcessJumpInput()
    {
        jumpInput = playerInputJump;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        playerInputMove = ctx.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext ctx)
    {
        playerInputLook = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        playerInputJump = ctx.performed;
    }
}
