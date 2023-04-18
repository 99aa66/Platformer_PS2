using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

public class EnemyHealthFusilli : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    public HealthBar healthBarEnnemy;
    [SerializeField] GameObject healthBarEnnemyObject;

    public SpriteRenderer SpriteEnnemi;
    public bool TakenDamage = false;

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
        healthBarEnnemyObject.SetActive(false);
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
    private void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.gameObject.CompareTag("Player"))
        {
            Head head = Player.gameObject.GetComponent<Head>();
            if (head != null && head.isAttacking)
            {
                EnemyHealthFusilli.instance.TakeDamage(10);
            }
        }
    }

    private IEnumerator ShowBar()
    {
        healthBarEnnemyObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        healthBarEnnemyObject.SetActive(false);
    }
}
