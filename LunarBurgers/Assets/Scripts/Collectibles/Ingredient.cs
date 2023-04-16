using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, ICollectable
{
    public string ingredientName;
    public Sprite ingredientIcon;
    private Vector3 startPosition;
    private Outline outline;

    public bool mustMove;

    private float frequency = 2f;
    private float magnitude = 0.2f;

    public delegate void Collected(Ingredient ingredient);
    public static event Collected OnCollected;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        outline = GetComponentInChildren<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mustMove) return;
        Movement();
    }

    public void OnInteraction()
    {
        OnCollected?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void Movement()
    {
        transform.position = startPosition + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    public void Reveal()
    {
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = Color.red;
    }

    public void Hide()
    {
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.black;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController)
        {
            OnInteraction();
        }
    }
}
