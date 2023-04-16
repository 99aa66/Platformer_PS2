using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) //Si joueur comportant tag player entre dans zone
        {
            Destroy(objectToDestroy);
        }
    }
}
