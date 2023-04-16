using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour, ICollectable
{
    public abstract void Movement();
    public abstract void OnInteraction();
}
