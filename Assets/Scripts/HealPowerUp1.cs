using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp1 : MonoBehaviour
{
    public int healthPoints;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collision.gameObject.layer == playerLayer)
        {
            PlayerHealth playerhealth = collision.transform.GetComponent<PlayerHealth>();
            // si la barre de vie est pleine, désactivez la gravité et la collision de l'orbe de vie
            if (playerhealth.currentHealth == playerhealth.maxHealth)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.isKinematic = true;
                GetComponent<Collider2D>().isTrigger = true;
            }
            // si la barre de vie n'est pas pleine, guérissez le joueur et détruisez l'orbe de vie
            else
            {
                playerhealth.HealPlayer(healthPoints);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.layer != playerLayer)
        {
            // L'orbe ne doit pas être détruite si elle touche un objet autre que "Player"
            return;
        }
    }
}