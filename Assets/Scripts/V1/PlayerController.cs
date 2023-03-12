using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    SpriteRenderer sr;
    public float jumpForce = 12f;
    public float playerSpeed;
    public Vector2 jumpHeight;
    private bool isOnGround;
    public float positionRadius;
    public LayerMask ground;
    public Transform playerPos;
    [SerializeField] bool Ground = false;
    [SerializeField] bool is_jumping = false;
    [Range(0, 1)][SerializeField] float smooth_time = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int k = i; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                anim.Play("Walk");
                rb.AddForce(Vector2.right * playerSpeed * Time.deltaTime);
                sr.flipX = false;
            }
            else
            {
                anim.Play("WalkBack");
                rb.AddForce(Vector2.left * playerSpeed * Time.deltaTime);
                sr.flipX = true;
            }
        }
        else
        {
            anim.Play("Idle");
        }

        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jumping");
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
        }
        if (Input.GetButtonDown("Jump"))
        {
            is_jumping = true;
        }
    }

    void FixedUpdate()
    {
        // Jump
        if (is_jumping)
        {
            is_jumping = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Ground = false;
        }
    }
}
