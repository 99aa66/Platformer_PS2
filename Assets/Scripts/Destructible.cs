using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public float destroyDelay = 1.5f;
    public int damageOnCollision = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var head1 = collision.gameObject.GetComponent<Head1>();
        var head = collision.gameObject.GetComponent<Head>();
        bool isAttacking = false;

        if (head1 != null)
        {
            isAttacking = head1.isAttacking;
        }
        else if (head != null)
        {
            isAttacking = head.isAttacking;
        }

        if (collision.gameObject.CompareTag("Player") && isAttacking)
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
