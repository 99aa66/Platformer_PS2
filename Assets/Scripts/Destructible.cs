using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public float destroyDelay = 2.0f;
    public int damageOnCollision = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Head>().isAttacking)
        {
            PlayerHealth.instance.TakeDamage(damageOnCollision);
            GameObject brokenObject = Instantiate(destroyedVersion,transform.position,transform.rotation);
            Destroy(brokenObject, destroyDelay);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Cafetière")
        {
            GameObject brokenObject = Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(brokenObject, destroyDelay);
            Destroy(gameObject);
        }
    }
}
