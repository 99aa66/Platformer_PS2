using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafetiereController : MonoBehaviour
{
    public int durability = 3;
    public bool isBroken { get { return durability <= 0; } }
    public bool isBeingHeld;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private const float transparentAlpha = 0.5f;

    public Animator anim;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // Récupération du composant Rigidbody2D
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    public void DecrementDurability()
    {
        durability--;
        if (isBroken)
        {
            anim.SetTrigger("break");
            sr.color = new Color(1f, 1f, 1f, transparentAlpha);
            ResetPosition();
        }
    }
    public void ResetPosition()
    {
        anim.SetTrigger("ResetPosition");
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        sr.color = Color.white;
        StartCoroutine(Static());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ennemi") || col.gameObject.CompareTag("Glass"))
        {
            DecrementDurability();
        }
        else if (col.gameObject.CompareTag("Ground") && col.relativeVelocity.magnitude > 50f)
        {
            anim.SetTrigger("break");
            ResetPosition();
        }
    }
    private IEnumerator Static()
    {
        rb.bodyType = RigidbodyType2D.Static;

        // Attendre 0,5 seconde pour que l'objet s'immobilise complètement
        yield return new WaitForSeconds(0.5f);

        // Réaffecter le rb par défaut à l'objet
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}