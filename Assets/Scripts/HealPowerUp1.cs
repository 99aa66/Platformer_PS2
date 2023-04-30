using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp1 : MonoBehaviour
{
    public int healthPoints;
    private bool isMoving = true;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-8, 8), 15);
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (isMoving && Vector2.Distance(transform.position, PlayerHealth.instance.transform.position) < 2f)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            isMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.instance.currentHealth += healthPoints;
            Destroy(gameObject);
        }
    }
}