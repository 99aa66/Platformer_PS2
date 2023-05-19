using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head1 : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private HingeJoint2D joint;
    [SerializeField] float distMax = 3;
    public GameObject playerPos;
    float restingAngle = 90f;
    public static bool isAttacking;

    public static Head1 instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'instance de Head dans la scène");
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

    private void Update()
    {
        Vector2 difference;
        Vector2 StickPos = new Vector2(Input.GetAxis("RStickHorizontal"), Input.GetAxis("RStickVertical"));
        if (StickPos != Vector2.zero)
        {
            difference = StickPos.normalized;
            // Calcul de l'angle de rotation de la tête en fonction de la direction de la souris
        }
        else
        {
            Vector2 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);  // Récupération de la position de la souris
            difference = (mousePos - (Vector2)playerPos.transform.position).normalized; // Calcul de la direction entre la position du joueur et celle de la souris
        }

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // Calcul de l'angle de rotation de la tête en fonction de la direction de la souris
        Debug.DrawRay((Vector2)playerPos.transform.position, difference); // Dessin d'un rayon pour visualiser la direction de la tête

        if (Input.GetButton("Clic gauche") || StickPos != Vector2.zero) // Si le bouton gauche de la souris est enfoncé
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, rotationZ)); // Rotation de la tête en fonction de la direction de la souris
            rb.MovePosition((Vector2)playerPos.transform.position + difference * distMax); // Déplacement de la tête dans la direction de la souris jusqu'à une certaine distance
            isAttacking = true;
        }
        else
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, 1500 * Time.deltaTime));  // Si le bouton gauche de la souris n'est pas enfoncé, la tête revient à sa position
            isAttacking = false;
        }
    }
}