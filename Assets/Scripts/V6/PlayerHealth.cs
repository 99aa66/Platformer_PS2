using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public float invincibilityFlashDelay = 0.2f;

    public HealthBar HealthBar;

    [SerializeField] GameObject hitboxDMG;

    // Start is called before the first frame update
    void Start()
    {
        // le joueur commence avec toute sa vie
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // test pour voir si ca fonctionne quand on prend des damages
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage (int damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;  // si on prend des degats ont retire de la vie a la vie actuelle
            HealthBar.SetHealth(currentHealth); // pour mettre a jour le visuel de la barre de vie
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            hitboxDMG.SetActive(false);
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);

            hitboxDMG.SetActive(true);
        }
        Debug.Log("Coroutine1");
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(1.5f);
        isInvincible = false;
        Debug.Log("Coroutine2");
    }
}
