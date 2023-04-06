using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public Barredeviecoq healthBar;
    [SerializeField] GameObject HealthBarEnnemy;

    public SpriteRenderer coquillette1;
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

    private void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.transform.name == "Player" && !TakenDamage)
        {
            TakeDamage(10);
            StartCoroutine(ShowBar());
            
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
            coquillette1.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);

            coquillette1.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }
}
