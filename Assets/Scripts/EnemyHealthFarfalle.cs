using System.Collections;
using UnityEngine;
public class EnemyHealthFarfalle : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public HealthBar healthBarEnnemy;

    private SpriteRenderer SpriteEnnemi;
    public bool takeDamage = false;

    private static EnemyHealthFarfalle instance;

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
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarEnnemy.SetHealth(currentHealth);

        if (!takeDamage)
        {
            takeDamage = true;
            StartCoroutine(ShowBar());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool isAttacking = Head1.isAttacking || (collision.gameObject.GetComponent<Head>()?.isAttacking ?? false);

            EnemyHealthFarfalle enemyHealth = GetComponent<EnemyHealthFarfalle>();

            if (isAttacking)
            {
                enemyHealth.TakeDamage(10);
            }
            else if (collision.gameObject.CompareTag("Cafeti�re") && collision.gameObject.GetComponent<CafetiereController>().GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                enemyHealth.TakeDamage(15);
            }
        }
    }
    private IEnumerator ShowBar()
    {
        healthBarEnnemy.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        healthBarEnnemy.gameObject.SetActive(false);
        takeDamage = false;
    }
}
