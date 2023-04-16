using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackPowerup : Powerup
{
    public delegate void Pickup();
    public static event Pickup OnPickup;

    float rotationSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
    }

    public override void OnInteraction()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController)
        {
            OnPickup.Invoke();
            gameObject.SetActive(false);
        }
    }
}
