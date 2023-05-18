using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movementForce;
    [SerializeField] float jumpForce;
    [Space(5)]
    [Range(0f, 100f)] public float raycastDistance = 1.5f;

    public float positionRadius;
    public bool isGrounded;
    public LayerMask Default;
    public bool can_jump = false;
    public Transform playerPos;
    public PlayerHealth playerHealth;

    [Header("Camera Follow")]
    private Camera cam;
    [Range(0f, 1f)] public float interpolation = 0.1f;

    [Header("Animation")]
    public Animator anim;

    public Rigidbody2D rb;
    public CameraFollow camFollow;

    public static PlayerController instance;

    [Header("Collider")]
    public PolygonCollider2D Hanchecol;
    public BoxCollider2D Bustecol;
    public BoxCollider2D Headcol;
    public PolygonCollider2D Top_Headcol;
    public PolygonCollider2D JambeDcol;
    public PolygonCollider2D TibiaDcol;
    public CapsuleCollider2D PiedDcol;
    public PolygonCollider2D JambeGcol;
    public PolygonCollider2D TibiaGcol;
    public CapsuleCollider2D PiedGcol;
    public CircleCollider2D Grabheadcol;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'instance de PlayerController dans la scène");
            return;
        }

        instance = this;
    }
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();

        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int k = i; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }

    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(playerPos.position, positionRadius, Default);
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            can_jump = true;
        }
        if (rb.velocity.y < -3f)
        {
            rb.gravityScale = 50;
        }
        else rb.gravityScale = 25;
    }

    private void FixedUpdate()
    {
        Movement();

        if (can_jump == true)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            can_jump = false; // là on saute plus car on est déjà en saut (éviter le double saut)
        }

        camFollow.UpdatePosition(transform.position);
    }
    private void Movement()
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xDir * (movementForce * Time.deltaTime), rb.velocity.y);

        // animation

        if (xDir > 0)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("WalkBack", false);
        }

        if (xDir < 0)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("WalkBack", true);
        }

        if (xDir == 0)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
            anim.SetBool("WalkBack", false);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerPos.position, positionRadius);
    }
}