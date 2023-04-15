using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour, IProcessInputHandler
{
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private Transform player;

    private Vector2 lookInput;
    [SerializeField] private float minClamp, maxClamp;
    [SerializeField] private float mouseSensitivity;

    private float cameraAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponentInParent<PlayerInputManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        HandleCameraMovement();
    }

    public void ProcessInput()
    {
        lookInput = inputManager.lookInput;
    }

    void HandleCameraMovement()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        cameraAngle += -mouseY;
        cameraAngle = Mathf.Clamp(cameraAngle, minClamp, maxClamp);

        transform.localRotation = Quaternion.Euler(cameraAngle, 0, 0);
        player.Rotate(Vector3.up * mouseX);
    }

}
