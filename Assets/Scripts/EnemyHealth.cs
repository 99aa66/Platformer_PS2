using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    [SerializeField] GameObject HealthBarEnnemy;

    public SpriteRenderer SpriteEnnemi;
    public float invincibilityFlashDelay = 0.2f;
    public bool TakenDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        HealthBarEnnemy.SetActive(false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (!TakenDamage)
        {
            TakenDamage = true;
            StartCoroutine(InvincibilityFlash());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Head playerHead = collision.gameObject.GetComponentInChildren<Head>();
            if (playerHead != null && playerHead.isAttacking)
            {
                TakeDamage(10);
                StartCoroutine(ShowBar());
            }
            else if (playerHead != null)
            {
                TakeDamage(5); // nouveau code pour les dégâts infligés en sautant sur l'ennemi
                StartCoroutine(ShowBar());
            }
        }
    }

        private IEnumerator ShowBar()
    {
        HealthBarEnnemy.SetActive(true);
        yield return new WaitForSeconds(5f);
        HealthBarEnnemy.SetActive(false);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (TakenDamage)
        {
            SpriteEnnemi.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);

            SpriteEnnemi.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }
}
