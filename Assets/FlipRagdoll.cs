using UnityEngine;

public class FlipRagdoll : MonoBehaviour
{
    private Rigidbody2D[] rigidbodies;

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody2D>();
    }
    public void Flip(bool flipX)
    {
        // D�sactive tous les joints
        Joint2D[] joints = GetComponentsInChildren<Joint2D>();
        foreach (Joint2D joint in joints)
        {
            joint.enabled = false;
        }

        // Arr�te tous les mouvements des rigidbodies
        foreach (Rigidbody2D rb in rigidbodies)
        {
            rb.velocity = Vector2.zero;
        }

        // Inverse l'axe x de l'objet si flipX est vrai
        Vector3 scale = transform.localScale;
        if (flipX)
        {
            scale.x *= -1;
        }
        scale.z += 180;
        transform.localScale = scale;

        // R�active tous les rigidbodies
        foreach (Rigidbody2D rb in rigidbodies)
        {
            rb.isKinematic = false;
        }

        // R�active tous les joints
        foreach (Joint2D joint in joints)
        {
            joint.enabled = true;
        }
    }
}