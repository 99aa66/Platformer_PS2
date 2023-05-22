using UnityEngine;

public class Head : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private HingeJoint2D joint;
    [SerializeField] float distMax = 3;
    public GameObject playerPos;
    float restingAngle = 90f;
    public bool isAttacking;
    private bool isOnClick = false;
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

    private void Update()
    {
        Vector2 difference;
        Vector2 StickPos = new Vector2(Input.GetAxis("RStickHorizontal"), Input.GetAxis("RStickVertical"));
        if (StickPos != Vector2.zero)
        {
            difference = StickPos.normalized;
            // Calcul de l'angle de rotation de la t�te en fonction de la direction de la souris
        }
        else
        {
            Vector2 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);  // R�cup�ration de la position de la souris
            difference = (mousePos - (Vector2)playerPos.transform.position).normalized; // Calcul de la direction entre la position du joueur et celle de la souris
        }

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // Calcul de l'angle de rotation de la t�te en fonction de la direction de la souris
        Debug.DrawRay((Vector2)playerPos.transform.position, difference); // Dessin d'un rayon pour visualiser la direction de la t�te

        if(Input.GetButtonDown("Clic gauche"))
        {
            isOnClick = true;
        }
        if ( (isOnClick || StickPos != Vector2.zero) && !isAttacking) // Si le bouton gauche de la souris est enfonc� et que la t�te n'est pas la t�te du haut du personnage
        {
            joint.enabled = false; // D�sactivation du joint qui relie la t�te au corps
            isAttacking = true; // Indique que la t�te est en train d'attaquer
        }
        if (Input.GetButtonUp("Clic gauche") || (StickPos == Vector2.zero && isAttacking && !isOnClick)) // Si le bouton gauche de la souris est rel�ch� et que la t�te n'est pas la t�te du haut du personnage
        {
            rb.MovePosition((Vector2)playerPos.transform.position); // La t�te revient � sa position de d�part
            rb.MoveRotation(0);
            joint.enabled = true; // R�activation du joint qui relie la t�te au corps
            isAttacking = false; // Indique que la t�te a fini d'attaquer
            isOnClick = false;
        }

        if (isAttacking) // Si le bouton gauche de la souris est enfonc�
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, rotationZ)); // Rotation de la t�te en fonction de la direction de la souris
        }
        else
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, 10000 * Time.deltaTime));  // Si le bouton gauche de la souris n'est pas enfonc�, la t�te revient � sa position
        }
    }
}