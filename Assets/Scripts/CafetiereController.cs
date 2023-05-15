using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafetiereController : MonoBehaviour
{
    public int durability = 5;
    public bool isBroken { get { return durability <= 0; } }
    public bool isBeingHeld;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private const float transparentAlpha = 0.5f;

    public Grabbing grabbingScript;

    public Animator anim;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // R�cup�ration du composant Rigidbody2D
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        grabbingScript= GetComponent<Grabbing>();
    }

    private void Start()
    {
        ResetDurability();
    }
    public void DecrementDurability()
    {
        durability--;
        if (isBroken)
        {
            anim.SetTrigger("break");
            sr.color = new Color(1f, 1f, 1f, transparentAlpha);
            anim.SetTrigger("ResetPosition");
            StartCoroutine(Static());
            ResetPosition();
        }
    }
    public void ResetPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        sr.color = Color.white;
        ResetDurability(); // R�initialiser la durabilit�
    }
    private void ResetDurability()
    {
        durability = 5;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ennemi") || col.gameObject.CompareTag("Glass") || col.gameObject.CompareTag("Ground"))
        {
            DecrementDurability();
            if (grabbingScript != null)
            {
                grabbingScript.DestroyJoint();
            }
        }
        /*else if (col.gameObject.CompareTag("Ground") && !isHoldingObject && col.relativeVelocity.magnitude > 50f)
        {
            isHoldingObject = false;
            anim.SetTrigger("break");
            ResetPosition();
        }*/
    }

    private IEnumerator Static()
    {
        rb.bodyType = RigidbodyType2D.Static;
        //rb.simulated = false;
        // Attendre 0,5 seconde pour que l'objet s'immobilise compl�tement
        yield return new WaitForSeconds(1f);
        //rb.simulated = true;
        // R�affecter le rb par d�faut � l'objet
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}