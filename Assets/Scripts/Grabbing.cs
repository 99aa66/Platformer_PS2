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
    private CafetiereController heldCafetiere;
    void Update()
    {
        if (Input.GetKeyDown(mouseButton))
        {
            canGrab = true;
        }
        if (Input.GetKeyUp(mouseButton))
        {
            canGrab = false;
            if (joint != null && currentlyHolding != null)
            {
                DestroyImmediate(joint); // supprimer imm�diatement le joint et ne pas attendre la fin de frame
                if (heldCafetiere != null) // la cafeti�re a �t� l�ch�e
                {
                    if (heldCafetiere.isBroken) // si la cafeti�re est cass�e, on la remet � sa position de d�part
                    {
                        heldCafetiere.ResetPosition();
                    }
                    heldCafetiere.isBeingHeld = false; // on indique que la cafeti�re n'est plus tenue
                    heldCafetiere = null;
                }
                joint = null;
                currentlyHolding = null;
            }
        }

        if (!canGrab && currentlyHolding != null && joint != null)
        {
            if (heldCafetiere != null) // la cafeti�re a �t� l�ch�e
            {
                heldCafetiere.isBeingHeld = false; // on indique que la cafeti�re n'est plus tenue
                heldCafetiere = null;
            }
            FixedJoint2D[] joints = currentlyHolding.GetComponents<FixedJoint2D>();
            for (int i = 0; i < joints.Length; i++)
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
        if (canGrab && col.gameObject.GetComponent<Rigidbody2D>() != null && !col.CompareTag("Player") && !col.CompareTag("Ennemi") && !col.CompareTag("Glass"))
        {
            if (col.gameObject.CompareTag("Cafeti�re"))
            {
                if (col.gameObject.GetComponent<CafetiereController>().isBroken)
                {
                    return; // la cafeti�re est cass�e, on ne peut pas la ramasser
                }
                currentlyHolding = col.gameObject;
                joint = currentlyHolding.AddComponent<FixedJoint2D>();
                joint.connectedBody = head;
                heldCafetiere = currentlyHolding.GetComponent<CafetiereController>();
                if (heldCafetiere != null)
                {
                    heldCafetiere.isBeingHeld = true;
                }
                Physics2D.SyncTransforms(); // synchroniser les positions et rotations des objets avant de cr�er le joint
            }
            else if (col.gameObject.CompareTag("Untagged")) // Ajout de cette condition pour d�tacher la cafeti�re si elle entre en collision avec un objet dont le tag est "Untagged"
            {
                if (heldCafetiere != null) // la cafeti�re a �t� l�ch�e
                {
                    if (heldCafetiere.isBroken) // si la cafeti�re est cass�e, on la remet � sa position de d�part
                    {
                        heldCafetiere.ResetPosition();
                    }
                    heldCafetiere.isBeingHeld = false; // on indique que la cafeti�re n'est plus tenue
                    heldCafetiere = null;
                }
                DestroyImmediate(joint); // supprimer imm�diatement le joint et ne pas attendre la fin de frame
                joint = null;
                currentlyHolding = null;
            }
            else
            {
                currentlyHolding = col.gameObject;
                joint = currentlyHolding.AddComponent<FixedJoint2D>();
                joint.connectedBody = head;
                Physics2D.SyncTransforms(); // synchroniser les positions et rotations des objets avant de cr�er le joint
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (currentlyHolding != null && col.gameObject == currentlyHolding)
        {
            if (heldCafetiere != null) //
            {
                if (heldCafetiere.isBroken)
                {
                    heldCafetiere.ResetPosition();
                }
                heldCafetiere.isBeingHeld = false;
                heldCafetiere = null;
            }
            joint = null;
            currentlyHolding = null;
        }
    }
}