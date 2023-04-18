using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageForBodyPart : MonoBehaviour
{
    public PlayerHealth playerHealth; // référence vers le script PlayerHealth
    public int damageAmount; // nombre de points de vie perdus lorsque ce collider est touché

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // vérifier si le collider qui touche celui-ci est un ennemi
        {
            playerHealth.TakeDamage(damageAmount); // faire perdre des points de vie au joueur
        }
    }
}
