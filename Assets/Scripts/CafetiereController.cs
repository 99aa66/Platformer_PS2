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

    private const float transparentAlpha = 0.5f;

    private bool activated = false;
    [SerializeField] Animator anim;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // R�cup�ration du composant Rigidbody2D
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Start()
    {
        ResetDurability();
    }
    public void DecrementDurability()
    {
        durability--;

        switch (durability)
        {
            case 5:
                sr.color = Color.white;
                Debug.Log("Color changed to yellow");
                break;
            case 4:
                sr.color = Color.yellow;
                Debug.Log("Color changed to yellow");
                break;
            case 3:
                sr.color = new Color(1f, 0.5f, 0f);
                Debug.Log("Color changed to orange");
                break;
            case 2:
                sr.color = new Color(1f, 0.3f, 0f);
                Debug.Log("Color changed to dark orange");
                break;
            case 1:
                sr.color = Color.red;
                Debug.Log("Color changed to red");
                break;
            default:
                anim.SetTrigger("break");
                sr.color = new Color(1f, 1f, 1f, transparentAlpha);
                anim.SetTrigger("ResetPosition");
                //StartCoroutine(Static());
                ResetPosition();
                break;
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
        durability = 7;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ennemi" || col.gameObject.tag == "Glass" || col.gameObject.tag == "Ground")
        {
            DecrementDurability();
        }
        else if (col.gameObject.tag == "Player" && !activated)
        {
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                activated = true;
            }
        }
    }
    /*private IEnumerator Static()
    {
        //yield return new WaitForSeconds(0.5f);
        rb.bodyType = RigidbodyType2D.Static;
        // Attendre 1 seconde pour que l'objet s'immobilise compl�tement
        yield return new WaitForSeconds(1f);
        // R�affecter le rb par d�faut � l'objet
        rb.bodyType = RigidbodyType2D.Dynamic;
    }*/
}