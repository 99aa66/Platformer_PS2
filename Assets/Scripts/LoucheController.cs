using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoucheController : MonoBehaviour
{
    private bool isTriggered = false;
    private Collider2D col;
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    public void ResetPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag("Player"))
        {
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            if (col != null)
            {
                col.isTrigger = true;
            }
            isTriggered = true;
        }
        else if(isTriggered && collision.gameObject.CompareTag("Ground"))
        {
            col.isTrigger = false;
            isTriggered = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isTriggered && (collision.gameObject.CompareTag("Player")))
        {
            if (col != null)
            {
                col.isTrigger = false;
            }
            isTriggered = false;
        }
    }
}
