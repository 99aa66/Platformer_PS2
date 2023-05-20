using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossMama : MonoBehaviour
{
    public GameObject objectToDrop; // L'objet que le joueur doit faire tomber sur le boss
    [SerializeField] float fallSpeed = 10f; // La vitesse à laquelle l'objet tombe

    private Rigidbody2D bossRb; // Le Rigidbody du boss
    private Collider2D bossCollider; // Le Collider du boss
    private bool isObjectDropped = false; // Indique si l'objet a été lâché ou non
    public GameObject porteCredits; // Référence à l'objet "Door" dans l'inspecteur Unity

    private bool isBossBlocked = false; // Variable de contrôle pour vérifier si le boss est bloqué

    public GameObject gameOverCanvas;
    private Animator fadeSystem;
    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Cafetière"))
        {
            Rigidbody2D bossRigidbody = GetComponent<Rigidbody2D>();
            bossRigidbody.bodyType = RigidbodyType2D.Static;

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
        else if (collision.collider.gameObject == objectToDrop && !isBossBlocked)
        {
            bossRb.velocity = Vector2.zero;
            isBossBlocked = true; // Le boss est bloqué
            porteCredits.SetActive(true); // Activation de l'objet porte crédits
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
}
