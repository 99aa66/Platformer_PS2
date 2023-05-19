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
            currentVelocity.y = 0f; // R�initialisez la composante Y de la v�locit� � z�ro
            rb.velocity = currentVelocity;
            Debug.Log("V�locit� en Y r�initialis�e � z�ro");
        }
    }
}
