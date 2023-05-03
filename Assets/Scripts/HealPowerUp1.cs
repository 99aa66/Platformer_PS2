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
    }

    private void FixedUpdate()
    {
        if (isMoving && PlayerHealth.instance != null && Vector2.Distance(transform.position, PlayerHealth.instance.transform.position) < 2f)
        {
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