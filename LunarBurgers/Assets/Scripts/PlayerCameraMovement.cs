using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour, IProcessInputHandler
{
    [SerializeField] private PlayerInputManager inputManager;

    private Vector2 lookInput;
    [SerializeField] private float minClamp, maxClamp;
    [SerializeField] private float mouseSensitivity;

    private float cameraAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponentInParent<PlayerInputManager>();
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
        cameraAngle += (lookInput.y * mouseSensitivity * Time.deltaTime);
        cameraAngle = Mathf.Clamp(cameraAngle, minClamp, maxClamp);
        transform.eulerAngles = new Vector3(cameraAngle, 0, 0);
    }

}
