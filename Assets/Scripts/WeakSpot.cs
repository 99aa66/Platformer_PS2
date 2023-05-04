using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Cafetière")) //Si joueur comportant tag player entre dans zone
        {
          Destroy(objectToDestroy);
        }
    }
}
