using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossMama : MonoBehaviour
{
    [SerializeField] GameObject objectToDrop; // L'objet que le joueur doit faire tomber sur le boss

    private Collider2D bossCollider; // Le Collider du boss
    private Rigidbody2D bossRb; // Le Rigidbody du boss
    private bool isBossBlocked = false; // Variable de contrôle pour vérifier si le boss est bloqué
    private Animator bossMamaAnimator;

    [SerializeField] GameObject porteCredits;
    [SerializeField] GameObject gameOverCanvas;
    private Animator fadeSystem;

    [SerializeField] Transform player;
    [SerializeField] bool isFlipped = false;
    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<Collider2D>();
        bossMamaAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            bossRb.bodyType = RigidbodyType2D.Static;

            Rigidbody2D[] rigidbodies = collision.transform.root.GetComponentsInChildren<Rigidbody2D>();
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
            GameObject playerRef = collision.transform.root.gameObject;
            StartCoroutine(ReplacePlayer(hancheRef));
        }
        else if (collision.collider.CompareTag("Cafetière"))
        {
            Rigidbody2D cafetiereRb = collision.collider.GetComponent<Rigidbody2D>();
            if (cafetiereRb != null && cafetiereRb.bodyType != RigidbodyType2D.Kinematic) //ignorer le gameOver si la cafetière est kinematic
            {
                bossRb.bodyType = RigidbodyType2D.Static;
                StartCoroutine(GameOver());
            }
        }
        else if (collision.collider.gameObject == objectToDrop && !isBossBlocked)
        {
            bossRb.bodyType = RigidbodyType2D.Static;
            isBossBlocked = true; // Le boss est bloqué
            bossMamaAnimator.SetBool("isBossBlocked", true);
            porteCredits.SetActive(true); // Activation de l'objet porte crédits
        }
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    private IEnumerator ReplacePlayer(Rigidbody2D hancheRef)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        hancheRef.transform.position = CurrentSceneManager.instance.respawnPoint;
        hancheRef.isKinematic = false;

        gameOverCanvas.SetActive(true);// Afficher le canvas GameOver
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator GameOver()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        gameOverCanvas.SetActive(true); // Afficher le canvas GameOver
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}