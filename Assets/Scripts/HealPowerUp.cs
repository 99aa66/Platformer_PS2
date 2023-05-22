using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int healthPoints;

    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth) // rendre de la vie au joueur + garder coeur sur map si max de vie
            {
                AudioManager.instance.PlayClipAt(sound, transform.position);
                PlayerHealth.instance.HealPlayer(healthPoints);
                Destroy(gameObject);
            }
        }
    }
}
