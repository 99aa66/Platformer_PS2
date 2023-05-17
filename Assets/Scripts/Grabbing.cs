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

    private FixedJoint2D jointhead;

    private void Start()
    {
        cafControl = GetComponent<CafetiereController>();
        jointhead = GetComponent<FixedJoint2D>();
    }
    void Update()
    {
        jointhead.enabled = false;
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
            jointhead.enabled = true;
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
            jointhead.enabled = true;
            jointhead.autoConfigureConnectedAnchor = false;
        }
        else if ((cafControl.gameObject != null || cafControl.durability <= 0))
        {
            DestroyJoint();
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