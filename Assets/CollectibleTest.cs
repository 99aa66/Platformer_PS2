using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTest : MonoBehaviour
{

    CollectibleInterface compteur;
    bool onlyOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        compteur = FindObjectOfType<CollectibleInterface>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(!onlyOnce)
            {
                onlyOnce = true;
                Debug.Log("Test");
                compteur.nbCollectible++;
                Destroy(gameObject);
            }
        }
    }
}
