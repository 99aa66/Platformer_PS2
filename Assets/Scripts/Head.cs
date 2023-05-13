using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Head : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private HingeJoint2D joint;
    [SerializeField] float distMax = 3;
    public GameObject playerPos;
    float restingAngle = 90f;
    public bool isAttacking;

    public static Head instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'instance de Head dans la sc�ne");
            return;
        }

        instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        joint = GetComponent<HingeJoint2D>();
        isAttacking = false;
    }
   void Update()
    {

        Vector2 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);  // R�cup�ration de la position de la souris
        Vector2 difference = (mousePos - (Vector2)playerPos.transform.position).normalized; // Calcul de la direction entre la position du joueur et celle de la souris
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // Calcul de l'angle de rotation de la t�te en fonction de la direction de la souris
        Debug.DrawRay((Vector2)playerPos.transform.position, difference); // Dessin d'un rayon pour visualiser la direction de la t�te

        if (Input.GetButtonDown("Clic gauche") && rb.gameObject.name != "Top_Head") // Si le bouton gauche de la souris est enfonc� et que la t�te n'est pas la t�te du haut du personnage
        {
            joint.enabled = false; // D�sactivation du joint qui relie la t�te au corps
            isAttacking = true; // Indique que la t�te est en train d'attaquer
            SendMessage("OnAttacked", SendMessageOptions.DontRequireReceiver); // On envoie un message aux objets pouvant �tre endommag�s pour les pr�venir de l'attaque
        }

        if (Input.GetButtonUp("Clic gauche") && rb.gameObject.name != "Top_Head") // Si le bouton gauche de la souris est rel�ch� et que la t�te n'est pas la t�te du haut du personnage
        {
            rb.MovePosition((Vector2)playerPos.transform.position); // La t�te revient � sa position de d�part
            rb.MoveRotation(0);
            joint.enabled = true; // R�activation du joint qui relie la t�te au corps
            isAttacking = false; // Indique que la t�te a fini d'attaquer
        }

        if (Input.GetButton("Clic gauche")) // Si le bouton gauche de la souris est enfonc�
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, rotationZ)); // Rotation de la t�te en fonction de la direction de la souris
            if (rb.gameObject.name == "Top_Head") // Si la t�te est la t�te du haut du personnage
            {
                rb.MovePosition((Vector2)playerPos.transform.position + difference * distMax); // D�placement de la t�te dans la direction de la souris jusqu'� une certaine distance
            }
        }
        else
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, 1500 * Time.deltaTime));  // Si le bouton gauche de la souris n'est pas enfonc�, la t�te revient � sa position
        }
    }

    /*void Update()
    {
        Vector2 StickPos = new Vector2(Input.GetAxis("RStickHorizontal"), Input.GetAxis("RStickVertical"));
        Vector2 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);  // R�cup�ration de la position de la souris
        Vector2 difference = (mousePos - (Vector2)playerPos.transform.position).normalized;// Calcul de la direction entre la position du joueur et celle de la souris
        if (StickPos != Vector2.zero)
        {
            difference = StickPos;
            // Calcul de l'angle de rotation de la t�te en fonction de la direction de la souris
        }


        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // Calcul de l'angle de rotation de la t�te en fonction de la direction de la souris
        Debug.DrawRay((Vector2)playerPos.transform.position, difference); // Dessin d'un rayon pour visualiser la direction de la t�te

        if ((Input.GetButtonDown("Clic gauche") || StickPos != Vector2.zero) && rb.gameObject.name != "Top_Head") // Si le bouton gauche de la souris est enfonc� et que la t�te n'est pas la t�te du haut du personnage
        {
            joint.enabled = false; // D�sactivation du joint qui relie la t�te au corps
            isAttacking = true; // Indique que la t�te est en train d'attaquer
            SendMessage("OnAttacked", SendMessageOptions.DontRequireReceiver); // On envoie un message aux objets pouvant �tre endommag�s pour les pr�venir de l'attaque
        }

        if ((Input.GetButtonUp("Clic gauche") || StickPos != Vector2.zero) && rb.gameObject.name != "Top_Head") // Si le bouton gauche de la souris est rel�ch� et que la t�te n'est pas la t�te du haut du personnage
        {
            rb.MovePosition((Vector2)playerPos.transform.position); // La t�te revient � sa position de d�part
            rb.MoveRotation(0);
            joint.enabled = true; // R�activation du joint qui relie la t�te au corps
            isAttacking = false; // Indique que la t�te a fini d'attaquer
        }

        if (Input.GetButton("Clic gauche") || StickPos != Vector2.zero) // Si le bouton gauche de la souris est enfonc�
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, rotationZ)); // Rotation de la t�te en fonction de la direction de la souris
            if (rb.gameObject.name == "Top_Head") // Si la t�te est la t�te du haut du personnage
            {
                rb.MovePosition((Vector2)playerPos.transform.position + difference * distMax); // D�placement de la t�te dans la direction de la souris jusqu'� une certaine distance
            }
        }
        else
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, 1500 * Time.deltaTime));  // Si le bouton gauche de la souris n'est pas enfonc�, la t�te revient � sa position
        }
    }*/
}