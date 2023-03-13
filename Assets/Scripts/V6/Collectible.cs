using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameObject playerRef;
    private GameObject Huile;

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
            playerRef.GetComponentInChildren<Vie>().pv += 10;
            Destroy(Huile);
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