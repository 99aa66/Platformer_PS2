using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private HingeJoint2D joint;
    [SerializeField] float distMax = 3;
    public GameObject playerPos;
    float restingAngle = 90f;
    public bool isAttacking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        joint = GetComponent<HingeJoint2D>();
    }
    void Update()
    {
        // R�cup�ration de la position de la souris
        Vector2 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        // Calcul de la direction entre la position du joueur et celle de la souris
        Vector2 difference = (mousePos - (Vector2)playerPos.transform.position).normalized;
        // Calcul de l'angle de rotation de la t�te en fonction de la direction de la souris
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Dessin d'un rayon pour visualiser la direction de la t�te
        Debug.DrawRay((Vector2)playerPos.transform.position, difference);

        // Si le bouton gauche de la souris est enfonc� et que la t�te n'est pas la t�te du haut du personnage
        if (Input.GetButtonDown("Clic gauche") && rb.gameObject.name != "Top_Head")
        {
            // D�sactivation du joint qui relie la t�te au corps
            joint.enabled = false;
            // Indique que la t�te est en train d'attaquer
            isAttacking = true;
            // On envoie un message aux objets pouvant �tre endommag�s pour les pr�venir de l'attaque.
            SendMessage("OnAttacked", SendMessageOptions.DontRequireReceiver);
        }

        // Si le bouton gauche de la souris est rel�ch� et que la t�te n'est pas la t�te du haut du personnage
        if (Input.GetButtonUp("Clic gauche") && rb.gameObject.name != "Top_Head")
        {
            // La t�te revient � sa position de d�part
            rb.MovePosition((Vector2)playerPos.transform.position);
            rb.MoveRotation(0);
            // R�activation du joint qui relie la t�te au corps
            joint.enabled = true;
            // Indique que la t�te a fini d'attaquer
            isAttacking = false;
        }

        // Si le bouton gauche de la souris est enfonc�
        if (Input.GetButton("Clic gauche"))
        {
            // Rotation de la t�te en fonction de la direction de la souris
            rb.MoveRotation(Quaternion.Euler(0, 0, rotationZ));
            // Si la t�te est la t�te du haut du personnage
            if (rb.gameObject.name == "Top_Head")
            {
                // D�placement de la t�te dans la direction de la souris jusqu'� une certaine distance
                rb.MovePosition((Vector2)playerPos.transform.position + difference * distMax);
            }
        }
        else
        {
            // Si le bouton gauche de la souris n'est pas enfonc�, la t�te revient � sa position
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, 1500 * Time.deltaTime));
        }
    }
}