using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Collectible : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        Player = GetComponent<GameObject>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    /*private GameObject HancheRef;

    // Update is called once per frame
    void Update()
    {
        PickUpPotion();
    }

    void PickUpPotion()
    {
        if (HancheRef && Input.GetKeyDown(KeyCode.E))
        {
            //HancheRef.GetComponentInChildren<Vie>().pv += 10; DEMANDER A MANU
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        HancheRef = collision.gameObject;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        HancheRef = null;
    }*/
}