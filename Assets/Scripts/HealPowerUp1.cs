using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp1 : MonoBehaviour
{
    public int healthPoints;
    Rigidbody2D rb;

    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // rendre de la vie au joueur + garder coeur sur map si max de vie
            if (PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
            {
                PlayerHealth.instance.HealPlayer(healthPoints);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}