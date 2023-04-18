using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

public class IAEnnemyFarfalle : MonoBehaviour
{
    float speeda = 3f;
    float distancea = 5f;
    private bool movingRight = true;
    public Transform groundDetection;
    public Transform target;
    public float speedb = 6f;
    float distancemax = 13f;
    public int damageOnCollision = 5;
    private EnemyHealth enemyHealth;

    bool atckType1 = true;
    private void function1()
    {
        transform.Translate(Vector2.right * speeda * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distancea);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    private void function2()
    {

        transform.position = Vector3.MoveTowards(transform.position, target.position, speedb * Time.deltaTime);
        if (transform.position.y == 3)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distancemax)

        {
            atckType1 = false;
        }
        else
        {
            atckType1 = true;
        }
        if (atckType1)
        {
            function1();
        }
        else
        {
            function2();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(damageOnCollision);
            Head head = collision.gameObject.GetComponent<Head>();
           if (head != null && head.isAttacking)
           {
             EnemyHealth.instance.TakeDamage(10);
           }
        }
    }
}

