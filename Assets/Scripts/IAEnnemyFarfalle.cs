using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnnemyFarfalle : MonoBehaviour
{
    [Header("Patrouille")]
    public Transform groundDetection;
    public Transform obstacleDetection;
    private Transform playerPos;
    private bool isFacingRight = true;
    public LayerMask Default;
    private bool isGrounded;
    private bool isObstacleAhead;
    public float moveSpeed = 3f;
    private float moveDirection = 1;
    [SerializeField] float circleRadius;

    [Header("Mouvement")]
    [SerializeField] float speed;
    public Transform target;
    private float height;
    Transform farfalle_transform;
    [SerializeField] bool is_attacking;
    private bool isOnGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;

    [Header("Attaque")]
    private int damageOnCollision = 10;
    [SerializeField] Transform attack_point;
    [SerializeField] LayerMask enemy_layers;
    float attack_range = 1.2f;
    float next_attack_time = 0f;

    [Header("Paramètre Farfalle")]
    Rigidbody2D farfalleRb;
    private Animator farfalleAnim;
    void Start()
    {
        farfalleRb = GetComponent<Rigidbody2D>();
        farfalleAnim = GetComponent<Animator>();
        farfalle_transform = transform;
        height = transform.position.y;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 15)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, height), speed * Time.deltaTime); // Suivre le joueur
        }
    }
    void FixedUpdate()
    {
        // Détection du sol, des obstacles et du joueur
        isGrounded = Physics2D.OverlapCircle(groundDetection.position, circleRadius, Default);
        isObstacleAhead = Physics2D.OverlapCircle(obstacleDetection.position, circleRadius, Default);
        isOnGround = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, Default);

        if (isOnGround) // Si on ne voit pas le joueur est qu'on est sur le sol, on continue à patrouiller
        {
            Patrouille();
        }

    }
    void Patrouille()
    {
        if (!isGrounded || isObstacleAhead)
        {
            if (isFacingRight)
            {
                Flip();
            }
            else if (!isFacingRight)
            {
                Flip();
            }
        }
        farfalleRb.velocity = new Vector2(moveSpeed * moveDirection, farfalleRb.velocity.y);
    }
    void FlipTowardsPlayer()
    {
        float playerPosition = playerPos.position.x - transform.position.x;
        if (playerPosition < 0 && isFacingRight)
        {
            Flip();
        }
        else if (playerPosition > 0 && !isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        moveDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    void Attack()
    {
        Collider2D[] hit_player = Physics2D.OverlapCircleAll(attack_point.position, attack_range, enemy_layers); // Liste des ennemis
        foreach (Collider2D player in hit_player) // Si joueur est touché
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damageOnCollision); // Faire des dégâts au player
        }
        next_attack_time = Time.time + 2f; // Limitation d'attaque (4 par secondes)
        is_attacking = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool isAttacking = (collision.gameObject.GetComponent<Head1>()?.isAttacking ?? false) || (collision.gameObject.GetComponent<Head>()?.isAttacking ?? false);

            if (!isAttacking)
            {
                PlayerHealth.instance.TakeDamage(damageOnCollision);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundDetection.position, circleRadius);
        Gizmos.DrawWireSphere(obstacleDetection.position, circleRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, boxSize);
    }
}