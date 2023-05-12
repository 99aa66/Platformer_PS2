using System.Collections;
using UnityEngine;

public class EnemyHealthFusilli : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public HealthBar healthBarEnnemy;

    private SpriteRenderer SpriteEnnemi;
    public bool takeDamage = false;
    public int damageOnCollision = 20;

    private static EnemyHealthFusilli instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a déjà une instance de EnemyHealthFusilli dans la scène");
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
            PlayerHealth playerhealth = collision.transform.GetComponent<PlayerHealth>();

            bool isAttacking = (collision.gameObject.GetComponent<Head1>()?.isAttacking ?? false) || (collision.gameObject.GetComponent<Head>()?.isAttacking ?? false);

            if (!isAttacking)
            {
                PlayerHealth.instance.TakeDamage(damageOnCollision);
            }
            else
            {
                Debug.Log("Player is attacking");
                GetComponent<EnemyHealthFusilli>().TakeDamage(10);
            }
            if (collision.gameObject.CompareTag("Cafetière") && collision.gameObject.GetComponent<CafetiereController>().GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                GetComponent<EnemyHealthFusilli>().TakeDamage(15);
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
