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

    // Start is called before the first frame update
    // Update is called once per frame
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
            isHoldingObject = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (canGrab && col.gameObject.GetComponent<Rigidbody2D>() != null && col.tag != "Player" && !isHoldingObject)
        {
            currentlyHolding = col.gameObject;
            isHoldingObject = true;

            FixedJoint2D[] joints = currentlyHolding.GetComponents<FixedJoint2D>(); // Vérifier si l'objet n'est pas déjà connecté à head
            bool alreadyConnected = false;
            for (int i = 0; i < joints.Length; i++)
            {
                if (joints[i].connectedBody == head)
                {
                    alreadyConnected = true;
                    break;
                }
            }

            if (!alreadyConnected) // Créer le joint si l'objet n'est pas déjà connecté à head
            {
                joint = currentlyHolding.AddComponent<FixedJoint2D>();
                joint.connectedBody = head;
            }
            // Rendre le rigidbody de la cafetière dynamique
            Rigidbody2D rb = currentlyHolding.GetComponent<Rigidbody2D>();
            if (rb != null && rb.isKinematic)
            {
                rb.isKinematic = false;
            }
        }
    }
}
