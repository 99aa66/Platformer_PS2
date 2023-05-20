using UnityEngine;

public class Grab : MonoBehaviour
{
    private bool hold;
    private FixedJoint2D currentJoint; // Stocker le joint actuel
    private Collider2D grabHeadCollider;

    private void Start()
    {
        grabHeadCollider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) || (Input.GetAxis("Grab")>0.5f))
        {
            hold = true;
        }
        else
        {
            hold = false;
            ReleaseObject(); // Appel fonction pour libérer l'objet actuel
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "BossMama" || collision.gameObject.tag == "BossLasagne")
        {
            return;
        }
        else
        {
            if (hold && currentJoint == null && collision.gameObject.GetComponent<Rigidbody2D>() != null) // Vérification si aucun joint actuel n'est déjà en place
            {
                Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
                if (rb != null || rb.isKinematic)
                {
                    rb.isKinematic = false;
                    currentJoint = gameObject.AddComponent<FixedJoint2D>(); // Ajout du joint à l'objet actuel
                    currentJoint.connectedBody = rb;
                }
            }
        }
    }

    private void ReleaseObject()
    {
        if (currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;
        }
    }
}