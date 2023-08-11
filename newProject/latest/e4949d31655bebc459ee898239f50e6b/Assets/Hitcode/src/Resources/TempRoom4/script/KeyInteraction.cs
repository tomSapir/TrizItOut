using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteraction : MonoBehaviour
{
    public GameObject candle;

    private bool isKeyClicked = false;
    private Rigidbody2D candleRigidbody;

    private void Start()
    {
        // Get the Rigidbody2D component of the candle
        candleRigidbody = candle.GetComponent<Rigidbody2D>();
        // Disable gravity for the candle initially
        candleRigidbody.gravityScale = 0f;
        // Make the candle kinematic initially (not affected by forces)
        candleRigidbody.isKinematic = true;
    }

    private void OnMouseDown()
    {
        if (!isKeyClicked)
        {
            // Hide the key
            gameObject.SetActive(false);

            // Drop the candle
            candleRigidbody.gravityScale = 1f;
            candleRigidbody.isKinematic = false;

            isKeyClicked = true;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isKeyClicked && collision.gameObject.CompareTag("Floor"))
        {
            // Candle collided with the floor, stop its movement
            candleRigidbody.isKinematic = true;
        }
    }
}

