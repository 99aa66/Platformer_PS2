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
    private bool isHoldingObject;

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
        // Vérifier si la cafetière est cassée
        if (currentlyHolding != null && currentlyHolding.CompareTag("Cafetière"))
        {
            CafetiereController cafetiereController = currentlyHolding.GetComponent<CafetiereController>();
            if (cafetiereController == null || (cafetiereController != null && cafetiereController.durability <= 0))
            {
                if (joint != null)
                {
                    Destroy(joint);
                    joint = null;
                }
                isHoldingObject = false;
                currentlyHolding = null;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (canGrab && !isHoldingObject)
        {
            if (col.CompareTag("Cafetière"))
            {
                currentlyHolding = col.gameObject;
                isHoldingObject = true;

                FixedJoint2D[] joints = currentlyHolding.GetComponents<FixedJoint2D>();
                bool alreadyConnected = false;
                for (int i = 0; i < joints.Length; i++)
                {
                    if (joints[i].connectedBody == head)
                    {
                        alreadyConnected = true;
                        break;
                    }
                }

                if (!alreadyConnected)
                {
                    joint = currentlyHolding.AddComponent<FixedJoint2D>();
                    joint.connectedBody = head;
                }

                Rigidbody2D rb = currentlyHolding.GetComponent<Rigidbody2D>();
                if (rb != null && rb.isKinematic)
                {
                    rb.isKinematic = false;
                }
            }
            else if (col.CompareTag("Player") || col.CompareTag("BossMama") || col.CompareTag("BossLasagne"))
            {
                // Do nothing
            }
            else
            {
                // ne rien faire si l'objet ne correspond à aucune condition
                return;
            }
        }
    }

}