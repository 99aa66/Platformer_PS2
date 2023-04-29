using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafetiereController : MonoBehaviour
{
    [SerializeField]public int durability = 4;
    public bool isBeingHeld = false;
    private Vector3 startPosition;
    private Quaternion startRotation;
    public bool isBroken
    {
        get
        {
            return durability <= 0;
        }
    }
    private void Start()
    {
        // Store the starting position and rotation of the object
        startPosition = transform.position;
        startRotation = transform.rotation;

        // Set the object active at the start of the game
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBeingHeld = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBeingHeld && collision.gameObject.CompareTag("Ground") && collision.relativeVelocity.magnitude > 10f)
        {
            gameObject.SetActive(false);
            Respawn();
        }
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            TakeDamage(1);
        }
    }
    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
    public void TakeDamage(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
            gameObject.SetActive(false);
            Respawn();
        }
    }
    private void Respawn()
    {
        // Disable the object for a few seconds
        gameObject.SetActive(false);
        Invoke("RespawnObject", 2f);
    }

    private void RespawnObject()
    {
        // Reset the durability
        durability = 4;
        // Reset the position and rotation of the object
        transform.position = startPosition;
        transform.rotation = startRotation;
        // Re-enable the object
        gameObject.SetActive(true);
    }
}
