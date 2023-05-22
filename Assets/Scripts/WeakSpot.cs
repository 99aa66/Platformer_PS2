using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;

    public AudioClip deadSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Cafetière"))
        {
            AudioManager.instance.PlayClipAt(deadSound, transform.position);
            Destroy(objectToDestroy);
        }
    }
}
