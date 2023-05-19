using UnityEngine;

public class RemoveVerticalVelocity : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector2 currentVelocity = rb.velocity;
            currentVelocity.y = 0f; // Réinitialisez la composante Y de la vélocité à zéro
            rb.velocity = currentVelocity;
            Debug.Log("Vélocité en Y réinitialisée à zéro");
        }
    }
}
