using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanche : MonoBehaviour
{
    private Rigidbody2D rb;
    float restingAngle = 90;
    float force = 500f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, force * Time.deltaTime));
    }

}
