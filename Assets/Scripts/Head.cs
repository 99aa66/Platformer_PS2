using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    int speed = 5;
    public Rigidbody2D rb;
    public Camera cam;
    public KeyCode mousebutton;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 difference = playerpos - transform.position;
        float rotationZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        if (Input.GetKey(mousebutton))
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.fixedDeltaTime));
        }
    }
}
