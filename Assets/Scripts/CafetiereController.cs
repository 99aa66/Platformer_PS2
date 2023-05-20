using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafetiereController : MonoBehaviour
{
    public int durability = 7;
    public bool isBroken { get { return durability <= 0; } }
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private float transparentAlpha = 0.5f;
    [SerializeField] Animator anim;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // Récupération du composant Rigidbody2D
        anim = GetComponent<Animator>();
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }
    public void DecrementDurability()
    {
        durability--;

        if (durability == 5)
        {
            sr.color = Color.white;
        }
        else if (durability == 4)
        {
            sr.color = Color.yellow;
        }
        else if (durability == 3)
        {
            sr.color = new Color(1f, 0.5f, 0f);
        }
        else if (durability == 2)
        {
            sr.color = new Color(1f, 0.3f, 0f);
        }
        else if (durability == 2)
        {
            sr.color = Color.red;
        }
        else if (durability == 1)
        {
            sr.color = Color.red;
        }
        else if (durability <= 0)
        {
            anim.SetTrigger("break");
            sr.color = new Color(1f, 1f, 1f, transparentAlpha);
            StartCoroutine(Static());
        }
    }

    public void ResetPosition()
    {
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
        sr.color = Color.white;
        rb.velocity = Vector2.zero;
        ResetDurability(); // Réinitialiser la durabilité
    }
    private void ResetDurability()
    {
        durability = 7;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ennemi" || col.gameObject.tag == "Glass" || col.gameObject.tag == "Ground")
        {
            DecrementDurability();
        }
    }
    private IEnumerator Static()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length); // Attendre la fin de l'animation "break" avant de faire réapparaîte cafetière à position d'origine
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("ResetPosition");
        ResetPosition();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}