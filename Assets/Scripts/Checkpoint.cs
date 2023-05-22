using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;

    public AudioClip waterSound;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>(); ;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(waterSound, transform.position);
            CurrentSceneManager.instance.respawnPoint = transform.position;
            anim.SetTrigger("Play");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
