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
    public SpriteRenderer oeil;
    public SpriteRenderer bouche;

    public float invicibilityTimeAfterHit = 3f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false; //perso pas invincible par défaut


    public HealthBar HealthBar;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }
    void Start()
    {
        // le joueur commence avec toute sa vie
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
    }

    
    void Update()
    {
        
    }

    public void HealPlayer(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth; //condition qui empêche de dépasser le nombre maximal de ppints de vie du joueur
        }
        else
        {
            currentHealth += amount;
        }
        
        HealthBar.SetHealth(currentHealth); // maj barre de vie

    }
    public void TakeDamage (int damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;  // si on prend des degats on retire de la vie a la vie actuelle
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
            oeil.color = new Color(1f, 1f, 1f, 0f);
            bouche.color = new Color(1f, 1f, 1f, 0f);
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
            oeil.color = new Color(1f, 1f, 1f, 1f);
            bouche.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvincible = false;
    }
}
