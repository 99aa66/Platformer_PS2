using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasagneAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            bool isAttacking = collision.gameObject.GetComponent<Head1>() != null && collision.gameObject.GetComponent<Head1>().isAttacking || collision.gameObject.GetComponent<Head>() != null && collision.gameObject.GetComponent<Head>().isAttacking;

            if (!isAttacking)
            {
                PlayerHealth.instance.TakeDamage(attackDamage);
            }
        }
        if (collision.gameObject.CompareTag("Cafetière"))
        {
            GetComponent<LasagneHealth>().TakeDamage(20);
        }
        if (collision.gameObject.CompareTag("Player") && ((collision.gameObject.GetComponent<Head>() != null && collision.gameObject.GetComponent<Head>().isAttacking) || (collision.gameObject.GetComponent<Head1>() != null && collision.gameObject.GetComponent<Head1>().isAttacking)))
        {
            GetComponent<LasagneHealth>().TakeDamage(15);
        }
    }
    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
