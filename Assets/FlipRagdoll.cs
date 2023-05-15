using UnityEngine;

public class FlipRagdoll : MonoBehaviour
{
    public Transform ragdollParent;
    public float flipSpeed = 5f;

    private bool isFacingRight = true;

    private void Update()
    {
        float xDir = Input.GetAxis("Horizontal");

        if (xDir < 0 && isFacingRight)
        {
            FlipCharacter();
        }
        else if (xDir > 0 && !isFacingRight)
        {
            FlipCharacter();
        }

        // Appliquer les forces de mouvement sur le personnage en fonction de la direction actuelle
        // ...
    }

    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;

        Vector3 newScale = ragdollParent.localScale;
        newScale.x *= -1;
        ragdollParent.localScale = newScale;
    }
}