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
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.isKinematic = true;
                GetComponent<Collider2D>().isTrigger = true;
            }
        }
        else if (!collision.gameObject.CompareTag("Player"))
        {
            // L'orbe ne doit pas être détruite si elle touche un objet autre que "Player"
            return;
        }
    }
}