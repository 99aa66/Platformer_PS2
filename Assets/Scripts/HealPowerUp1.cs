using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp1 : MonoBehaviour
{
    public int healthPoints;
    Rigidbody2D rb;
    Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
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
            else
            {
                // Ignorer les collisions entre le joueur et l'objet
                Physics2D.IgnoreCollision(collision.collider, col, true);

                // Passer en mode kinématique et activer la propriété isTrigger
                rb.bodyType = RigidbodyType2D.Kinematic;
                col.isTrigger = true;
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            if (collision.gameObject.CompareTag("Player") && PlayerHealth.instance.currentHealth == PlayerHealth.instance.maxHealth)
            {
                gameObject.layer = LayerMask.NameToLayer("Default"); // définir la couche de collision de l'objet sur "Default"
                collision.gameObject.layer = LayerMask.NameToLayer("Player"); // définir la couche de collision du joueur sur "NoCollision"
            }
        }
    }
}