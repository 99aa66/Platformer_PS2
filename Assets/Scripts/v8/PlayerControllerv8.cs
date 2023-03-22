using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerv8 : MonoBehaviour
{
    [Header("Movement")]
    public float movementForce;
    public float jumpForce = 10f;
    [Space(5)]
    [Range(0f, 100f)] public float raycastDistance = 1.5f;
    public LayerMask whatIsGround;

    public Vector2 jumpHeight;
    private bool isOnGround;
    public float positionRadius;
    [SerializeField] bool Ground = false;
    [SerializeField] bool is_jumping = false;
    [Range(0, 1)][SerializeField] float smooth_time = 0.5f;
    public Transform playerPos;

    [Header("Camera Follow")]
    private Camera cam;
    [Range(0f, 1f)] public float interpolation = 0.1f ;
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    [Header("Animation")]
    public Animator anim;
    public Transform hanche;
    /*public Transform head;
    public Transform top_head;
    public Transform buste;
    public Transform hanche;
    public Transform jambeD;
    public Transform tibiaD;
    public Transform piedD;
    public Transform jambeG;
    public Transform tibiaG;
    public Transform piedG;
    public Transform brasD;
    public Transform avb_D;
    public Transform mainD;
    public Transform brasG;
    public Transform avb_G;
    public Transform mainG;
    public Transform Zito;
    private float x = 0f;
    private float y = 0f;
    private float z = 0f;*/


    private Rigidbody2D rb;

    

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody2D>(); 
    }

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
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("jumping");
        }

        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, whatIsGround);
        if (isOnGround == true && Input.GetButtonDown("Jump"))
        {
            Debug.Log("jumping");
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
        }
        if (Input.GetButtonDown("Jump"))
        {
            is_jumping = true;
        }
    }
    private void FixedUpdate()
    {
        Movement();
        //Jump();
        CameraFollow();

        if (is_jumping)
        {
            is_jumping = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Ground = false;
        }
    }

    private void Movement()
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xDir * (movementForce * Time.deltaTime), rb.velocity.y);
        if (xDir!= 0) 
        {
            /*transform.localRotation = Quaternion.Euler(0, -180, 0);
            Zito.localScale = new Vector3(xDir, 1f, 1f);
            head.localScale = new Vector3(xDir, 1f, 1f);
            top_head.localScale = new Vector3(xDir, 1f, 1f);
            buste.localScale = new Vector3(xDir, 1f, 1f);
            hanche.localScale = new Vector3(xDir, 1f, 1f);
            jambeD.localScale = new Vector3(xDir, 1f, 1f);
            tibiaD.localScale = new Vector3(xDir, 1f, 1f);
            jambeG.localScale = new Vector3(xDir, 1f, 1f);
            tibiaG.localScale = new Vector3(xDir, 1f, 1f);
            piedD.localScale = new Vector3(xDir, 1f, 1f);
            piedG.localScale = new Vector3(xDir, 1f, 1f);
            brasD.localScale = new Vector3(xDir, 1f, 1f);
            avb_D.localScale = new Vector3(xDir, 1f, 1f);
            brasG.localScale = new Vector3(xDir, 1f, 1f);
            avb_G.localScale = new Vector3(xDir, 1f, 1f);
            mainD.localScale = new Vector3(xDir, 1f, 1f);
            mainG.localScale = new Vector3(xDir, 1f, 1f);*/
        }
    }

    /*private void Jump ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            }
           

        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, whatIsGround);
        return hit.collider != null;
    }*/

    private void CameraFollow()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position + offset, interpolation);
    }
}
