using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerv8 : MonoBehaviour
{
    [Header("Movement")]
    public float movementForce;
    public float jumpForce = 7f;
    [Space(5)]
    [Range(0f, 100f)] public float raycastDistance = 1.5f;
    
    //public Vector2 jumpHeight;

    public float positionRadius;
    public bool can_jump;
    public LayerMask Default;
    [SerializeField] bool is_jumping = false;
    [Range(0, 1)][SerializeField] float smooth_time = 0.5f;
    public Transform playerPos;

    [Header("Camera Follow")]
    private Camera cam;
    [Range(0f, 1f)] public float interpolation = 0.1f ;
    public Vector3 offset = new Vector3(0f, 2f, -7f);

    [Header("Animation")]
    public Animator anim;

    private Rigidbody2D rb;

    public ParticleSystem particleSystem; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        cam = Camera.main;

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
        can_jump = Physics2D.OverlapCircle(playerPos.position, positionRadius, Default);
        if (can_jump == true && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
            is_jumping = true;
        }

        // Check if the player is moving and enable/disable particle system
        float xDir = Input.GetAxisRaw("Horizontal");
        if (xDir != 0 && particleSystem.isStopped)
        {
            particleSystem.Play();
        }
        else if (xDir == 0 && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }
    private void FixedUpdate()
    {
        Movement();
        CameraFollow();

        if (is_jumping == true)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            is_jumping = false; // là on saute plus car on est déjà en saut (éviter le double saut)
        }
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

        // Particle System
        if (xDir != 0)
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }
    }
    private void CameraFollow()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position + offset, interpolation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerPos.position, positionRadius);
    }
}
