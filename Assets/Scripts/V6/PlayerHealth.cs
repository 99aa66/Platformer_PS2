using UnityEngine;
using System.Collections;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Sprite Renderer")]
    public SpriteRenderer JambeD;
    public SpriteRenderer PiedD;
    public SpriteRenderer BrasD;
    public SpriteRenderer MainD;
    public SpriteRenderer Corps;
    public SpriteRenderer BrasG;
    public SpriteRenderer MainG;
    public SpriteRenderer JambeG;
    public SpriteRenderer PiedG;

    public float invicibilityTimeAfterHit = 3f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false; //perso pas invincible par défaut


    public HealthBar HealthBar;

    //[SerializeField] GameObject hitboxDMG;

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
        if (Input.GetKeyDown(KeyCode.L))
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
            JambeD.color = new Color(1f, 1f, 1f, 0f); // RGB inchangé et A vaut 0 donc transparent
            PiedD.color = new Color(1f, 1f, 1f, 0f);
            BrasD.color = new Color(1f, 1f, 1f, 0f);
            MainD.color = new Color(1f, 1f, 1f, 0f);
            Corps.color = new Color(1f, 1f, 1f, 0f);
            BrasG.color = new Color(1f, 1f, 1f, 0f);
            MainG.color = new Color(1f, 1f, 1f, 0f);
            JambeG.color = new Color(1f, 1f, 1f, 0f);
            PiedG.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay); 

            JambeD.color = new Color(1f, 1f, 1f, 1f); // réafficher le perso en opaque
            PiedD.color = new Color(1f, 1f, 1f, 1f);
            BrasD.color = new Color(1f, 1f, 1f, 1f);
            MainD.color = new Color(1f, 1f, 1f, 1f);
            Corps.color = new Color(1f, 1f, 1f, 1f);
            BrasG.color = new Color(1f, 1f, 1f, 1f);
            MainG.color = new Color(1f, 1f, 1f, 1f);
            JambeG.color = new Color(1f, 1f, 1f, 1f);
            PiedG.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvincible = false;
    }
}
