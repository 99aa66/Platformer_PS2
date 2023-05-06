using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DeathZone : MonoBehaviour
{
    private Animator fadeSystem;
    private bool triggered = false;
    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //vérifier si c'est le joueur qui est entré en contact dans la zone, collision = player
        {
            if (!triggered)
            {
                triggered = true;
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
        }
        if (collision.CompareTag("Cafetière") || collision.CompareTag("Louche"))
        {
            if (!triggered)
            {
                triggered = true;
                if (collision.CompareTag("Cafetière"))
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
                StartCoroutine(RespawnObject(collision.gameObject, collision.GetComponent<Rigidbody2D>()));
            }
        }
    }
    private IEnumerator ReplacePlayer(Rigidbody2D hancheRef)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        hancheRef.transform.position = CurrentSceneManager.instance.respawnPoint; //position objet collision replacer au position de respawn
        hancheRef.isKinematic = false;
        triggered = false;
    }
    private IEnumerator RespawnObject(GameObject obj, Rigidbody2D rb)
    {
        // Désactive l'objet et attend un certain délai
        obj.SetActive(false);
        yield return new WaitForSeconds(1f);

        // Réinitialise la position et la vélocité de l'objet
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Réactive l'objet après un court délai
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(true);

        // Réinitialise le déclencheur pour permettre une nouvelle collision
        triggered = false;
    }
}
