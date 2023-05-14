using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebond : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb.sharedMaterial.bounciness = 0.05f; //Modifier cette valeur pour ajuster la force de restitution
    }
}
