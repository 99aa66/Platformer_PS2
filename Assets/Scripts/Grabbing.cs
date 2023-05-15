using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D head;
    public KeyCode mouseButton;
    private bool canGrab;
    private FixedJoint2D joint;

    private GameObject currentlyHolding;
    private bool isHoldingObject;
    private CafetiereController cafControl;

    private void Start()
    {
        cafControl = GetComponent<CafetiereController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(mouseButton) && !isHoldingObject)
        {
            canGrab = true;

        }
        if (Input.GetKeyUp(mouseButton))
        {
            canGrab = false;
            if (joint != null)
            {
                Destroy(joint);
                joint = null;
            }
        }

        if (!canGrab && currentlyHolding != null )
        {
            FixedJoint2D[] joints = currentlyHolding.GetComponents<FixedJoint2D>();
            for (int i = 0; i < joints.Length; i++)
            {
                if (joints[i].connectedBody == head && joints[i].isActiveAndEnabled)
                {
                    Destroy(joints[i]);
                }
            }
            joint = null;
            currentlyHolding = null;
            isHoldingObject = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        cafControl = col.gameObject.GetComponent<CafetiereController>();

        if (!canGrab || isHoldingObject)
        {
            return;
        }

        if (col.CompareTag("Cafetière"))
        {
            currentlyHolding = col.gameObject;
            isHoldingObject = true;

            FixedJoint2D[] joints = currentlyHolding.GetComponents<FixedJoint2D>();
            bool alreadyConnected = false;

            foreach (FixedJoint2D j in joints)
            {
                if (j.connectedBody == head)
                {
                    alreadyConnected = true;
                    break;
                }
            }

            if (!alreadyConnected)
            {
                joint = currentlyHolding.AddComponent<FixedJoint2D>();
                joint.connectedBody = head;
                joint.autoConfigureConnectedAnchor = false;
            }

            Rigidbody2D rb = currentlyHolding.GetComponent<Rigidbody2D>();

            if (rb != null && rb.isKinematic)
            {
                rb.isKinematic = false;
            }

            else if ((cafControl.gameObject != null || cafControl.durability <= 0) && alreadyConnected)
            {
                DestroyJoint();
            }
        }

        else if (col.CompareTag("Player") || col.CompareTag("BossMama") || col.CompareTag("BossLasagne"))
        {
            return;
        }
    }
    public void DestroyJoint()
    {
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }
        isHoldingObject = false;  // on indique que l'on ne tient plus d'objet
        currentlyHolding = null;
    }
}