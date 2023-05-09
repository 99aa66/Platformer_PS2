using System.Collections;
using UnityEngine;
public class EnemyHealthFarfalle : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public HealthBar healthBarEnnemy;

    public SpriteRenderer SpriteEnnemi;
    public bool TakenDamage = false;

    public static EnemyHealthFarfalle instance;

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

        if (!TakenDamage)
        {
            TakenDamage = true;
            StartCoroutine(ShowBar());
        }
    }

    private IEnumerator ShowBar()
    {
        healthBarEnnemy.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        healthBarEnnemy.gameObject.SetActive(false);
    }
}
