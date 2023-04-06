using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPatrolAI : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public float attackDistance = 1.5f;
    public float bounceForce = 5f;
    public float attackDelay = 1f;
    public int damageOnCollision = 20; // au moment de la collision il y a dégâts -20
    public SpriteRenderer graphics;
    private int direction = 1;

    private Transform target;
    //private int destPoint = 0;
    private Rigidbody2D rb;
    private bool canAttack = true;
    public int health = 40;
    // Start is called before the first frame update
    void Start()
    {
        //Commencer par le premier waypoints de la liste
        target = waypoints[0];
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // Calculate the direction of movement
        Vector3 dir = (waypoints[direction].position - transform.position).normalized;

        // Move the enemy
        transform.Translate(dir * speed * Time.deltaTime, Space.World);

        // Flip the graphics if necessary
        if (dir.x > 0)
        {
            graphics.flipX = true;
        }
        else if (dir.x < 0)
        {
            graphics.flipX = false;
        }

        // Check if the enemy has reached a waypoint
        float dist = Vector3.Distance(transform.position, waypoints[direction].position);
        if (dist < 0.1f)
        {
            // Change the direction of movement
            direction = (direction + 1) % waypoints.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            AttackPlayer(playerHealth);

            health -= damageOnCollision;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void AttackPlayer(PlayerHealth playerHealth)
    {
        speed = 0;
        // Deal damage to the player
        playerHealth.TakeDamage(damageOnCollision);

        // Wait for a few seconds before resuming movement
        StartCoroutine(WaitForAttackCooldown());
    }

    private IEnumerator WaitForAttackCooldown()
    {
        yield return new WaitForSeconds(3f);
        speed = 2f;
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator Cooldown2 (PlayerHealth playerHealth)
    {
        // Bounce towards player
        Vector2 bounceDirection = (target.position - transform.position).normalized;
        rb.velocity = new Vector2(bounceDirection.x * -bounceForce, bounceDirection.y * bounceForce);

        yield return new WaitForSeconds(attackDelay);

        canAttack = true;
    }
}
