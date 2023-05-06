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
        if (collision.CompareTag("Player")) //v�rifier si c'est le joueur qui est entr� en contact dans la zone, collision = player
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
        if (collision.CompareTag("Cafeti�re") || collision.CompareTag("Louche"))
        {
            if (!triggered)
            {
                triggered = true;
                if (collision.CompareTag("Cafeti�re"))
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
        // D�sactive l'objet et attend un certain d�lai
        obj.SetActive(false);
        yield return new WaitForSeconds(1f);

        // R�initialise la position et la v�locit� de l'objet
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // R�active l'objet apr�s un court d�lai
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(true);

        // R�initialise le d�clencheur pour permettre une nouvelle collision
        triggered = false;
    }
}
