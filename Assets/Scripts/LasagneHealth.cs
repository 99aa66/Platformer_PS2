using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasagneHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public HealthBar healthBarEnnemy;

    [SerializeField] SpriteRenderer SpriteEnnemi;
    public bool TakenDamage = false;

    public static LasagneHealth instance;
    public bool isInvulnerable = false;
    public Rigidbody2D HealPowerUp_1;
    public GameObject objectToDestroy;
    [SerializeField] bool isInvincible;
    [SerializeField] float invicibilityTimeAfterHit = 2f;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBarEnnemy.SetMaxHealth(maxHealth);
        isInvincible = false;
    }
    void Update()
    {
        if (currentHealth <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
    void TakeDamage(int damage)
    {
        if (isInvulnerable || isInvincible)
        {
            return;
        }
        
        currentHealth -= damage;
        healthBarEnnemy.SetHealth(currentHealth);

        if (!TakenDamage)
        {
            TakenDamage = true;
        }
        if (currentHealth <= 110)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
        isInvincible = true;
        Invoke("EndInvincibility", invicibilityTimeAfterHit); // Démarre un minuteur pour désactiver l'invincibilité après 0,3 seconde.
    }
    void Die()
    {
        for (int i =0; i<= Random.Range(0,6);i++)
        {
            Rigidbody2D H_HealPowerUp = Instantiate(HealPowerUp_1, transform.position, transform.rotation);
            H_HealPowerUp.velocity = new Vector2(Random.Range(-10, 10), 20);
        }
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(objectToDestroy);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Head1 head1 = collision.gameObject.GetComponent<Head1>();
            Head head = collision.gameObject.GetComponent<Head>();
            bool isAttacking = (head1 != null && head1.isAttacking) || (head != null && head.isAttacking);

            LasagneHealth lasagneHealth = GetComponent<LasagneHealth>();

            if (isAttacking)
            {
                lasagneHealth.TakeDamage(10);
            }
            else if (collision.gameObject.CompareTag("Cafetière") && collision.gameObject.GetComponent<CafetiereController>().GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                lasagneHealth.TakeDamage(15);
            }
        }
    }
    void EndInvincibility()
    {
        isInvincible = false;
    }
}