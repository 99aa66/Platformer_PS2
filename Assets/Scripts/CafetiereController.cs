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

    //private const string defaultLayerName = "Cafetière";
    private const string brokenLayerName = "BrokenCafetiere";
    private const float transparentAlpha = 0.5f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // Récupération du composant Rigidbody2D
    }
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    public void DecrementDurability()
    {
        durability--;
        if (isBroken)
        {
            gameObject.layer = LayerMask.NameToLayer("BrokenCafetiere");
            sr.color = new Color(1f, 1f, 1f, transparentAlpha);
            ResetPosition();
        }
    }
    void ResetPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        durability = 3;
        sr.color = Color.white;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ennemi"))
        {
            DecrementDurability();
        }
        else if (col.gameObject.CompareTag("Ground") && col.relativeVelocity.magnitude > 10f)
        {
            ResetPosition();
        }
    }
}