using UnityEngine;
using System.Collections;
using static Unity.Burst.Intrinsics.X86.Sse4_2;
using UnityEditor;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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

    public float invicibilityTimeAfterHit = 2f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false; //perso pas invincible par défaut

    public HealthBar HealthBar;

    [SerializeField] private Animator fadeSystem;

    private Vector3 lastCheckpoint;

    public static PlayerHealth instance;
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    public void Start()
    {
        // le joueur commence avec toute sa vie
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(60);
        }
    }
    public void HealPlayer(int amount)
    {
        if ((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth; //condition qui empêche de dépasser le nombre maximal de ppints de vie du joueur
        }
        else
        {
            currentHealth += amount;
        }

        HealthBar.SetHealth(currentHealth); // maj barre de vie

    }
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;  // si on prend des degats on retire de la vie a la vie actuelle
            HealthBar.SetHealth(currentHealth); // mettre a jour le visuel de la barre de vie
            //vérifier si le joueur est toujours vivant
            if (currentHealth <= 0) //si personnage a un point de vie, peut prendre des vies en négative
            {
                Die();
                return;//on a pas envie de rejouer le flash et le delay, mais direct animation de mort
            }
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        PlayerController.instance.enabled = false; //bloquer les mouvements du personnage en désactivant le script PlayerController
        Head.instance.enabled = false;
        Head1.instance.enabled = false;
        JambeD.color = new Color(1f, 1f, 1f, 0f);
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
        PlayerController.instance.anim.SetTrigger("Die");// jouer l'animation de mort dont l'animator a été récupérer du script PlayerController
        PlayerController.instance.rb.velocity = Vector3.zero; //velocité rb à 0
        PlayerController.instance.Hanchecol.enabled = false;
        PlayerController.instance.Bustecol.enabled = false;
        PlayerController.instance.Headcol.enabled = false;
        PlayerController.instance.Top_Headcol.enabled = false;
        PlayerController.instance.JambeDcol.enabled = false;
        PlayerController.instance.TibiaDcol.enabled = false;
        PlayerController.instance.PiedDcol.enabled = false;
        PlayerController.instance.JambeGcol.enabled = false;
        PlayerController.instance.TibiaGcol.enabled = false;
        PlayerController.instance.PiedGcol.enabled = false;
        Rigidbody2D[] rigidbodies = transform.root.GetComponentsInChildren<Rigidbody2D>();
        Rigidbody2D hancheRef = null;
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            if (rigidbodies[i].gameObject.name == "Hanche")
            {
                hancheRef = rigidbodies[i];
                break;
            }
        }
        hancheRef.isKinematic = true;
        hancheRef.velocity = Vector3.zero;
        StartCoroutine(RespawnPlayer(hancheRef));
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
    private IEnumerator RespawnPlayer(Rigidbody2D hancheRef)
    {
        yield return new WaitForSeconds(1.5f);
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        // replacer le joueur au dernier checkpoint
        hancheRef.transform.position = CurrentSceneManager.instance.respawnPoint; //position objet collision replacer au position de respawn
        hancheRef.isKinematic = false;
        PlayerController.instance.enabled = true; //bloquer les mouvements du personnage en désactivant le script PlayerController
        Head.instance.enabled = true;
        Head1.instance.enabled = true;
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
        PlayerController.instance.anim.SetTrigger("Idle");
        PlayerController.instance.Hanchecol.enabled = true;
        PlayerController.instance.Bustecol.enabled = true;
        PlayerController.instance.Headcol.enabled = true;
        PlayerController.instance.Top_Headcol.enabled = true;
        PlayerController.instance.JambeDcol.enabled = true;
        PlayerController.instance.TibiaDcol.enabled = true;
        PlayerController.instance.PiedDcol.enabled = true;
        PlayerController.instance.JambeGcol.enabled = true;
        PlayerController.instance.TibiaGcol.enabled = true;
        PlayerController.instance.PiedGcol.enabled = true;
        // réinitialiser la santé
        currentHealth = maxHealth;
        HealthBar.SetHealth(currentHealth);
    }
}
