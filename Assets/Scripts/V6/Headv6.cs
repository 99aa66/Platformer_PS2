using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headv6 : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private HingeJoint2D joint;
    [SerializeField] float distMax = 3;
    public GameObject playerPos;

    public float timePassed;
    public float waitTime;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        joint = GetComponent<HingeJoint2D>();
        timePassed = 0.05f;
        waitTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector2 difference = (mousePos - (Vector2)playerPos.transform.position).normalized;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Debug.DrawRay((Vector2)playerPos.transform.position, difference);



        if(Input.GetButtonDown("Clic gauche") && rb.gameObject.name != "Top_Head")
        {
            joint.enabled = false;         
            timePassed -= Time.fixedDeltaTime;

        }

        if (timePassed < 0)
        {
            StartCoroutine(waitingTime());
        }

        if (Input.GetButtonUp("Clic gauche") && rb.gameObject.name != "Top_Head")
        {
            rb.MovePosition((Vector2)playerPos.transform.position);
            rb.MoveRotation(0);
            joint.enabled = true;
        }



        if (Input.GetButton("Clic gauche"))
        {

            rb.MoveRotation(Quaternion.Euler(0, 0, rotationZ));
            if (rb.gameObject.name == "Top_Head")
            {
                rb.MovePosition((Vector2)playerPos.transform.position + difference*distMax);
            }
        }
        else
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, 90, 40 * Time.deltaTime));
        }

    }

  
    public IEnumerator waitingTime()
    {
        yield return new WaitForSeconds(2f);
        //return timePassed;

    }
}
