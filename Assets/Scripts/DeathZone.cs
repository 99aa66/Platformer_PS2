using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    private Animator fadeSystem;
    private bool triggered = false;
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) //vérifier si c'est le joueur qui est entré en contact dans la zone, collision = player
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
    }

    private IEnumerator ReplacePlayer(Rigidbody2D hancheRef)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        hancheRef.transform.position = playerSpawn.position; //position objet collision replacer au position du PlayerSpawn
        hancheRef.isKinematic = false;
        triggered = false;
    }
}
