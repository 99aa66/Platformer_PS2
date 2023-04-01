using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public Barredeviecoq healthBar;
    [SerializeField] GameObject HealthBarEnnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) //Si joueur comportant tag player entre dans zone
        {
            //TakeDamage(10);
            StartCoroutine(ShowBar());
            Destroy(objectToDestroy);
        }
    }
    private IEnumerator ShowBar()
    {
        HealthBarEnnemy.SetActive(true);
        yield return new WaitForSeconds(5f);
        HealthBarEnnemy.SetActive(false);
    }
}
