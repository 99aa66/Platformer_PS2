using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAEnnemy : MonoBehaviour
{
    float speeda = 3f;
    float distancea = 1f;
    private bool movingRight = true;
    public Transform groundDetection;
    public Transform target;
    public float speedb = 15f;
    float distancemax = 10f;

    public int startingHealth = 40;
    public int health;
    
    public int damageOnCollision = 20;
    bool atckType1 = true;
    void Start()
    {
        health = startingHealth;
    }
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
        /*transform.position = Vector3.MoveTowards(transform.position, target.position, speedb * Time.deltaTime);
        if (transform.position.y < 3.1f && transform.position.y > 2.9f)
        {
            transform.position = new Vector3(transform.position.x, 3f, transform.position.z);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);

            // decrease enemy health and update UI
            health -= damageOnCollision;
        }
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distancemax)

        {
            atckType1 = false;
            //if(Input.GetKeyDown(KeyCode.P))
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
}

