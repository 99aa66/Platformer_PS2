using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance3 : MonoBehaviour
{
    public float targetRotation;
    Rigidbody2D rb;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.deltaTime)); 
    }
}
