using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FlipSR : MonoBehaviour
{

    SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0) sr.flipX = false;
            else if (Input.GetAxisRaw("Horizontal") < 0) sr.flipX = true;
        }
    }
}
