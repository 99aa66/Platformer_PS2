using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public GameObject Jambe_D;
    public GameObject Jambe_G;
    Rigidbody2D Jambe_DRB;
    Rigidbody2D Jambe_GRB;
    public Rigidbody2D rb;

    public Animator anim;

    [SerializeField] float speed = 10f;
    [SerializeField] float stepWait = 0.3f;
    [SerializeField] float jumpForce = 10;
    private bool isOnGround;
    public float positionRadius;
    public LayerMask Ground;
    public Transform playerPos;


    // Start is called before the first frame update
    void Start()
    {
        Jambe_DRB = Jambe_D.GetComponent<Rigidbody2D>();
        Jambe_GRB = Jambe_G.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                anim.Play("Walk right");
                StartCoroutine(MoveRight(stepWait));

            }
            else
            {
                anim.Play("Walk left");
                StartCoroutine(MoveLeft(stepWait));

            }
        }
        else
        {
            anim.Play("Waving");
        }

        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, Ground);
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jumping");
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
        }

    }

    IEnumerator MoveRight(float seconds)
    {
        Jambe_GRB.AddForce(Vector2.right * (speed * 700) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        Jambe_DRB.AddForce(Vector2.right * (speed * 700) * Time.deltaTime);
    }

    IEnumerator MoveLeft(float seconds)
    {
        Jambe_DRB.AddForce(Vector2.left * (speed * 700) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        Jambe_GRB.AddForce(Vector2.left * (speed * 700) * Time.deltaTime);
    }
}
