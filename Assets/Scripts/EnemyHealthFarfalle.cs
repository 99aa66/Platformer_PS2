using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

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
    private void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.gameObject.CompareTag("Player"))
        {
            Head head = Player.gameObject.GetComponent<Head>();
            if (head != null && head.isAttacking)
            {
                EnemyHealthFarfalle.instance.TakeDamage(10);
            }
        }
    }

    private IEnumerator ShowBar()
    {
        healthBarEnnemy.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        healthBarEnnemy.gameObject.SetActive(false);
    }
}
