using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private Animator fadeSystem;
    public GameObject gameOverCanvas;
    private bool triggered = false;
    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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

        else if (collision.CompareTag("Cafetière"))
        {
            CafetiereController cafetiere = collision.GetComponent<CafetiereController>();
            if (cafetiere != null)
            {
                cafetiere.ResetPosition();
            }
        }
        else if (collision.CompareTag("Louche"))
        {
            LoucheController louche = collision.GetComponent<LoucheController>();
            if (louche != null)
            {
                louche.ResetPosition();
            }
        }
    } 
    private IEnumerator ReplacePlayer(Rigidbody2D hancheRef)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        hancheRef.transform.position = CurrentSceneManager.instance.respawnPoint;
        hancheRef.isKinematic = false;
        triggered = false;

        // Afficher le canvas GameOver
        gameOverCanvas.SetActive(true);
        // Attendre 2 secondes avant de relancer le jeu
        yield return new WaitForSeconds(2f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverCanvas.SetActive(false);
    }
}
