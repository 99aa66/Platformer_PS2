using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp1 : MonoBehaviour
{
    public int healthPoints;
    Rigidbody2D rb;
    [SerializeField] bool OnGround;
    [SerializeField] Transform ground_check;
    [SerializeField] LayerMask what_is_ground;
    float check_radius = 0.7f;

    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        OnGround = Physics2D.OverlapCircle(ground_check.position, check_radius, what_is_ground); // vérfier si objet de soin est au sol

        if (OnGround)
        {
            rb.bodyType = RigidbodyType2D.Static; // au contact du sol, objet de soin devient immobile
        }
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
    }
}