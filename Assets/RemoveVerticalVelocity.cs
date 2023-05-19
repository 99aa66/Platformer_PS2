using UnityEngine;

public class RemoveVerticalVelocity : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 currentVelocity = rb.velocity;
        currentVelocity.z = 0f; // Affectez zéro à la composante Z
        rb.velocity = currentVelocity;
    }
}
