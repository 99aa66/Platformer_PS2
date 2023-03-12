using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    CapsuleCollider2D cap;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animController;
    float horizontal_value;
    float vertical_value;
    Vector2 ref_velocity = Vector2.zero;
    float jumpForce = 9f;
    [SerializeField] TrailRenderer tr;
    [SerializeField] float moveSpeed_horizontal = 400.0f;
    [SerializeField] bool is_jumping = false;
    [SerializeField] bool grounded = false;
    [SerializeField] bool is_crouching = false;
    [Range(0, 1)][SerializeField] float smooth_time = 0.5f;
    float Climb_speed = 100.0f;
    public bool isLadder = false;
    public bool canClimb = false;
    bool CheckSphere;
    private Vector2 aidepose;
    [SerializeField] GameObject aide;

    // Start is called before the first frame update
    void Start()
    {
        cap = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_value = Input.GetAxis("Horizontal");
        vertical_value = Input.GetAxis("Vertical");

        if (horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;

        animController.SetFloat("Speed", Mathf.Abs(horizontal_value));

        if (Input.GetButtonDown("Jump") && grounded && !is_crouching)
        {
            is_jumping = true;
        }
    }
    void FixedUpdate()
    {
        // Jump
        if (is_jumping && grounded && !canClimb)
        {
            is_jumping = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animController.SetBool("Jumping", true);
            grounded = false;
        }

        // Crouch
        if (Input.GetButton("Vertical") && grounded)
        {
            Crouch();
        }

        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.deltaTime, rb.velocity.y);

        // Climb
        if (canClimb && vertical_value != 0)
        {
            rb.gravityScale = 0f;
            grounded = false;
            target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.deltaTime, vertical_value * Climb_speed * Time.deltaTime);
            animController.SetBool("Climbing", true);
        }

        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.05f);
    }
    private void Crouch()
    {
        aidepose = new Vector2(aide.transform.position.x, aide.transform.position.y);
        CheckSphere = Physics2D.OverlapCircle(aidepose, 0.1f);
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && grounded)
        {
            is_crouching = true;
            moveSpeed_horizontal = 200f;
            cap.offset = new Vector2(0.1f, -0.6f);
            cap.size = new Vector2(1.1f, 0.8f);
            cap.direction = CapsuleDirection2D.Horizontal;
            animController.SetBool("Crouching", true);
        }
        else if (CheckSphere == false)
        {
            is_crouching = false;
            moveSpeed_horizontal = 400f;
            cap.offset = new Vector2(0f, -0.35f);
            cap.size = new Vector2(1f, 1.3f);
            cap.direction = CapsuleDirection2D.Vertical;
            animController.SetBool("Crouching", false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
        animController.SetBool("Jumping", false);

        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = true;
            grounded = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = false;
            grounded = true;
            rb.gravityScale = 3f;
            animController.SetBool("Climbing", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animController.SetBool("Jumping", false);
    }
}