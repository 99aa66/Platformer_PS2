using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpFusilliAttacking : MonoBehaviour
{
    [Header("Patrouille")]
    public Transform groundDetection;
    public Transform obstacleDetection;
    public Transform playerDetection;
    private bool isFacingRight = true;
    [SerializeField] LayerMask groundLayer;
    private bool isGrounded;
    private bool isObstacleAhead;
    public float moveSpeed = 3f;
    private float moveDirection = 1;
    [SerializeField] float circleRadius;

    [Header("Jump Attaque")]
    [SerializeField] float jumpHeight;
    [SerializeField] Transform Player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    private bool isOnGround;

    [Header("Player vu")]
    [SerializeField] Vector2 lineOfSite;
    private bool isPlayerDetected;
    [SerializeField] LayerMask playerLayer;

    [Header("Paramètre Fusilli")]
    public int damageOnCollision = 20; //au moment de la collision il y a dégâts -20
    private Rigidbody2D fusilliRB;
    private Animator fusilliAnim;
    private EnemyHealthFusilli ennemyHealthFusilli;
    void Start()
    {
        fusilliRB = GetComponent<Rigidbody2D>();
        fusilliAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Détection du sol, des obstacles et du joueur
        isGrounded = Physics2D.OverlapCircle(groundDetection.position, circleRadius, groundLayer);
        isObstacleAhead = Physics2D.OverlapCircle(obstacleDetection.position, circleRadius, groundLayer);
        isOnGround = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);
        isPlayerDetected = Physics2D.OverlapBox(transform.position, lineOfSite, 0, playerLayer);

        AnimationController();
       
        if(!isPlayerDetected && isOnGround) // Si on ne voit pas le joueur est qu'on est sur le sol, on continue à patrouiller
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
        fusilliRB.velocity = new Vector2(moveSpeed * moveDirection, fusilliRB.velocity.y);
    }
    void JumpAttack()
    {float distanceFromPlayer = Player.position.x - transform.position.x;

        if (isOnGround)
        {
            fusilliRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse); // nouv vecteur (axe x, axe y, force non continue)
        }
    }
    void FlipTowardsPlayer()
    {
        float playerPosition = Player.position.x - transform.position.x;
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
    void AnimationController()
    {
        fusilliAnim.SetBool("isPlayerDetected", isPlayerDetected);
        fusilliAnim.SetBool("IsOnGround", isOnGround);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerhealth = collision.transform.GetComponent<PlayerHealth>();

            bool isAttacking = collision.gameObject.GetComponent<Head1>() != null && collision.gameObject.GetComponent<Head1>().isAttacking || collision.gameObject.GetComponent<Head>() != null && collision.gameObject.GetComponent<Head>().isAttacking;

            if (!isAttacking)
            {
                PlayerHealth.instance.TakeDamage(damageOnCollision);
            }
        }
        if (collision.gameObject.CompareTag("Cafetière"))
        {
            GetComponent<EnemyHealthFusilli>().TakeDamage(15);
        }
        if (collision.gameObject.CompareTag("Player") && ((collision.gameObject.GetComponent<Head>() != null && collision.gameObject.GetComponent<Head>().isAttacking) || (collision.gameObject.GetComponent<Head1>() != null && collision.gameObject.GetComponent<Head1>().isAttacking)))
        {
            GetComponent<EnemyHealthFusilli>().TakeDamage(10);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundDetection.position, circleRadius);
        Gizmos.DrawWireSphere(obstacleDetection.position, circleRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSite);
    }
}