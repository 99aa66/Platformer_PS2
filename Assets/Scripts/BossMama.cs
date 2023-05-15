using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossMama : MonoBehaviour
{
    public GameObject objectToDrop; // L'objet que le joueur doit faire tomber sur le boss
    public float fallSpeed = 10f; // La vitesse � laquelle l'objet tombe
    public float minDistanceToDrop = 5f; // La distance minimale � laquelle l'objet doit se trouver pour pouvoir �tre l�ch�

    private Rigidbody2D bossRb; // Le Rigidbody du boss
    private Collider2D bossCollider; // Le Collider du boss
    private bool isObjectDropped = false; // Indique si l'objet a �t� l�ch� ou non

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

    private void Update()
    {
        if (isObjectDropped)
        {
            // V�rifier si l'objet est � la distance minimale pour �tre l�ch�
            float distanceToPlayer = Vector2.Distance(transform.position, objectToDrop.transform.position);
            if (distanceToPlayer <= minDistanceToDrop)
            {
                // V�rifier si le joueur a appuy� sur la touche pour l�cher l'objet
                if (Input.GetButtonDown("Clic gauche"))
                {
                    // L�cher l'objet en ajoutant une force vers le bas
                    objectToDrop.GetComponent<Rigidbody2D>().isKinematic = false;
                    objectToDrop.GetComponent<Rigidbody2D>().AddForce(Vector2.down * fallSpeed, ForceMode2D.Impulse);
                    isObjectDropped = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Cafeti�re"))
        {
            // D�sactiver le Rigidbody2D pour que le corps de BossMama ne bouge pas
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
        else if (collision.collider.gameObject == objectToDrop)
        {
            // L'objet touche le boss, celui-ci ne peut plus bouger
            bossRb.velocity = Vector2.zero;
            bossCollider.enabled = false;
        }
    }
    private IEnumerator ReplacePlayer(Rigidbody2D hancheRef)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        hancheRef.transform.position = CurrentSceneManager.instance.respawnPoint;
        hancheRef.isKinematic = false;

        // Afficher le canvas GameOver
        gameOverCanvas.SetActive(true);
        // Attendre 2 secondes avant de relancer le jeu
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
