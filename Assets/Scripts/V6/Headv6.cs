using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headv6 : MonoBehaviour
{
    public float speed = 300f;
    public Camera cam;
    public Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 difference = (mousePos - transform.position);
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Debug.DrawRay(transform.position, difference);
        if (Input.GetButton("Clic gauche"))
        {
            rb.MoveRotation(Quaternion.Euler(0,0,rotationZ-90));
        }
        else
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, 0, 700 * Time.deltaTime));
        }
    }
    //  if (Input.GetButtonDown("Clic gauche") || Input.GetButtonDown("Grab"))
}
