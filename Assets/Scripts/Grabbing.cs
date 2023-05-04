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
            if (joint != null)
            {
                Destroy(joint);
                joint = null;
            }
        }

        if (!canGrab && currentlyHolding != null)
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
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (canGrab && col.gameObject.GetComponent<Rigidbody2D>() != null && col.tag != "Player")
        {
            currentlyHolding = col.gameObject;
            
            FixedJoint2D[] joints = currentlyHolding.GetComponents<FixedJoint2D>(); // V�rifier si l'objet n'est pas d�j� connect� � head
            bool alreadyConnected = false;
            for (int i = 0; i < joints.Length; i++)
            {
                if (joints[i].connectedBody == head)
                {
                    alreadyConnected = true;
                    break;
                }
            }
           
            if (!alreadyConnected) // Cr�er le joint si l'objet n'est pas d�j� connect� � head
            {
                joint = currentlyHolding.AddComponent<FixedJoint2D>();
                joint.connectedBody = head;
            }
            // Rendre le rigidbody de la cafeti�re dynamique
            Rigidbody2D rb = currentlyHolding.GetComponent<Rigidbody2D>();
            if (rb != null && rb.isKinematic)
            {
                rb.isKinematic = false;
            }
        }
    }
}