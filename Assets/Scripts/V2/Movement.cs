using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject JambeD;
    public GameObject JambeG;
    Rigidbody2D JambeDRB;
    Rigidbody2D JambeGRB;
    public Rigidbody2D rb;

    public Animator anim;

    [SerializeField] float speed = 10f;
    [SerializeField] float stepWait = 0.3f;
    [SerializeField] float jumpForce = 10;
    private bool isOnGround;
    public float positionRadius;
    public LayerMask ground;
    public Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        JambeDRB = JambeD.GetComponent<Rigidbody2D>();
        JambeGRB = JambeG.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                anim.Play("WalkRight");
                StartCoroutine(MoveRight(stepWait));
               
            }
            else
            {
                anim.Play("WalkLeft");
                StartCoroutine(MoveLeft(stepWait));

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

    }

    IEnumerator MoveRight(float seconds)
    {
        JambeGRB.AddForce(Vector2.right * (speed * 700) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        JambeDRB.AddForce(Vector2.right * (speed * 700) * Time.deltaTime);
    }

    IEnumerator MoveLeft(float seconds)
    {
        JambeDRB.AddForce(Vector2.left * (speed * 700) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        JambeGRB.AddForce(Vector2.left * (speed * 700) * Time.deltaTime);
    }
}
