using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>(); ;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CurrentSceneManager.instance.respawnPoint = transform.position;
            anim.SetTrigger("Play");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }
}
