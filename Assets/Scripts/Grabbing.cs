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
            if (col.gameObject.CompareTag("Cafetière"))
            {
                if (col.gameObject.GetComponent<CafetiereController>().isBroken)
                {
                    return; // la cafetière est cassée, on ne peut pas la ramasser
                }
                currentlyHolding = col.gameObject;
                joint = currentlyHolding.AddComponent<FixedJoint2D>();
                joint.connectedBody = head;
                col.gameObject.GetComponent<CafetiereController>().isBeingHeld = true;
            }
            else
            {
                currentlyHolding = col.gameObject;
                joint = currentlyHolding.AddComponent<FixedJoint2D>();
                joint.connectedBody = head;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (currentlyHolding != null && col.gameObject == currentlyHolding)
        {
            currentlyHolding.GetComponent<CafetiereController>()?.ResetPosition(); // on remet la cafetière à sa position de départ si c'est une cafetière
            if (currentlyHolding.GetComponent<CafetiereController>()?.isBroken == true)
            joint = null;
            currentlyHolding = null;
        }
    }
}
