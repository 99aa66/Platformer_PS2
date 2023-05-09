using System.Collections;
using UnityEngine;

public class EnemyHealthFusilli : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public HealthBar healthBarEnnemy;

    public SpriteRenderer SpriteEnnemi;
    public bool isInvincible = false;
    public float invicibilityTimeAfterHit = 0.5f;

    public static EnemyHealthFusilli instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a déjà une instance de EnemyHealth dans la scène");
            return;
        }

        instance = this;
    }
    void Start()
    {
        healthBarEnnemy.gameObject.SetActive(false);
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
        currentHealth -= damage;
        healthBarEnnemy.SetHealth(currentHealth);

        if (!isInvincible)
        {
            isInvincible = true;
            StartCoroutine(ShowBar());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }
    private IEnumerator ShowBar()
    {
        healthBarEnnemy.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        healthBarEnnemy.gameObject.SetActive(false);
    }
    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvincible = false;
    }
}
