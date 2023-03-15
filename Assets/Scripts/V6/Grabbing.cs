using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    public Rigidbody2D head;
    public KeyCode mouseButton;
    private bool canGrab; 
    private FixedJoint2D joint;

    private GameObject currentlyHolding;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(mouseButton))
        {
            canGrab = true;
        }
        if (Input.GetKeyUp(mouseButton))
        {
            canGrab = false;
        }

        if (!canGrab && currentlyHolding != null) 
        { 
            FixedJoint2D[] joints = currentlyHolding.GetComponents<FixedJoint2D>();
            for (int i = 0; i< joints.Length; i++)
            {
                if (joints[i].connectedBody == head)
                {
                    Destroy(joints[i]);
                }
            }
            joint = null;
            currentlyHolding = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (canGrab && col.gameObject.GetComponent<Rigidbody2D>() != null && col.tag != "Player")
        {
            currentlyHolding = col.gameObject;
            joint = currentlyHolding.AddComponent<FixedJoint2D>();
            joint.connectedBody = head;
        }
    }



}
