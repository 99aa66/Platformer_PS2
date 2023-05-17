using UnityEngine;
using System.Collections;

public class LoucheController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private bool activated = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    public void ResetPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        StartCoroutine(Static());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                activated = true;
            }
        }
        if(collision.gameObject.tag == "Ground")
        {
            rb.bodyType = RigidbodyType2D.Static;

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
