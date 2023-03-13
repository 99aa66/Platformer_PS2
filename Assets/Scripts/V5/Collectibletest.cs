using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibletest : MonoBehaviour
{
    private GameObject playerRef;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PickUpPotion();
    }

    void PickUpPotion()
    {
        if (playerRef && Input.GetKeyDown(KeyCode.E))
        {
            //playerRef.GetComponentInChildren<Health>().pv += 10;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        playerRef = collision.gameObject;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        playerRef = null;
    }
}
