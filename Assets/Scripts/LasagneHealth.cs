using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasagneHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public HealthBar healthBarEnnemy;

    public SpriteRenderer SpriteEnnemi;
    public bool TakenDamage = false;

    public static LasagneHealth instance;
    public bool isInvulnerable = false;

    public Rigidbody2D HealPowerUp_1;
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
    }
    void Update()
    {
        if (currentHealth <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

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
    }
    void Die()
    {
        for (int i =0; i<= Random.Range(0,4);i++)
        {
            Rigidbody2D H_HealPowerUp = Instantiate(HealPowerUp_1, transform.position, transform.rotation);
            H_HealPowerUp.velocity = new Vector2(Random.Range(-15, 15), 25);
        }
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject);
    }
}