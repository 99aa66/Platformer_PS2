using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grabbing : MonoBehaviour
{
    [SerializeField] Rigidbody2D head;
    public KeyCode mouseButton;
    private bool canGrab;
    private FixedJoint2D jointhead;

    private GameObject currentlyHolding;
    private bool isHoldingObject;
    private bool isJointActive;

    private void Update()
    {
        if (Input.GetKeyDown(mouseButton))
        {
            canGrab = true;
        }
        if (Input.GetKeyUp(mouseButton))
        {
            canGrab = false;
            ReleaseObject();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (canGrab && currentlyHolding == null && col.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            currentlyHolding = col.gameObject;
            jointhead = head.gameObject.AddComponent<FixedJoint2D>();
            jointhead.connectedBody = currentlyHolding.GetComponent<Rigidbody2D>();
        }
    }

    private void ReleaseObject()
    {
        if (currentlyHolding != null && jointhead != null)
        {
            Destroy(jointhead);
            jointhead = null;
            currentlyHolding = null;
        }
    }
    /*void Update()
    {
        if (!isJointActive)
        {
            jointhead.enabled = false;
        }
        if (Input.GetKeyDown(mouseButton) && !isHoldingObject)
        {
            canGrab = true;
            jointhead.enabled = true;
        }
        if (Input.GetKeyUp(mouseButton))
        {
            canGrab = false;
            jointhead.enabled = false;
        }

        if (!canGrab)
        {
            isJointActive = false;
            jointhead.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (canGrab && col.gameObject.GetComponent<Rigidbody2D>() != null && !isHoldingObject)
        {
            isHoldingObject = true;

            if (jointhead.connectedBody != col.gameObject.GetComponent<Rigidbody2D>())
            {
                jointhead.connectedBody = col.gameObject.GetComponent<Rigidbody2D>();
            }
        }
        else if (col.CompareTag("Player") || col.CompareTag("BossMama") || col.CompareTag("BossLasagne"))
        {
            return;
        }
    }*/
}