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
        // Récupération de la position de la souris
        Vector2 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        // Calcul de la direction entre la position du joueur et celle de la souris
        Vector2 difference = (mousePos - (Vector2)playerPos.transform.position).normalized;
        // Calcul de l'angle de rotation de la tête en fonction de la direction de la souris
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Dessin d'un rayon pour visualiser la direction de la tête
        Debug.DrawRay((Vector2)playerPos.transform.position, difference);

        // Si le bouton gauche de la souris est enfoncé et que la tête n'est pas la tête du haut du personnage
        if (Input.GetButtonDown("Clic gauche") && rb.gameObject.name != "Top_Head")
        {
            // Désactivation du joint qui relie la tête au corps
            joint.enabled = false;
            // Indique que la tête est en train d'attaquer
            isAttacking = true;
            // On envoie un message aux objets pouvant être endommagés pour les prévenir de l'attaque.
            SendMessage("OnAttacked", SendMessageOptions.DontRequireReceiver);
        }

        // Si le bouton gauche de la souris est relâché et que la tête n'est pas la tête du haut du personnage
        if (Input.GetButtonUp("Clic gauche") && rb.gameObject.name != "Top_Head")
        {
            // La tête revient à sa position de départ
            rb.MovePosition((Vector2)playerPos.transform.position);
            rb.MoveRotation(0);
            // Réactivation du joint qui relie la tête au corps
            joint.enabled = true;
            // Indique que la tête a fini d'attaquer
            isAttacking = false;
        }

        // Si le bouton gauche de la souris est enfoncé
        if (Input.GetButton("Clic gauche"))
        {
            // Rotation de la tête en fonction de la direction de la souris
            rb.MoveRotation(Quaternion.Euler(0, 0, rotationZ));
            // Si la tête est la tête du haut du personnage
            if (rb.gameObject.name == "Top_Head")
            {
                // Déplacement de la tête dans la direction de la souris jusqu'à une certaine distance
                rb.MovePosition((Vector2)playerPos.transform.position + difference * distMax);
            }
        }
        else
        {
            // Si le bouton gauche de la souris n'est pas enfoncé, la tête revient à sa position
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, 1500 * Time.deltaTime));
        }
    }
}