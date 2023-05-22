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

        if(Input.GetButtonDown("Clic gauche"))
        {
            isOnClick = true;
        }
        if ( (isOnClick || StickPos != Vector2.zero) && !isAttacking) // Si le bouton gauche de la souris est enfoncé et que la tête n'est pas la tête du haut du personnage
        {
            joint.enabled = false; // Désactivation du joint qui relie la tête au corps
            isAttacking = true; // Indique que la tête est en train d'attaquer
        }
        if (Input.GetButtonUp("Clic gauche") || (StickPos == Vector2.zero && isAttacking && !isOnClick)) // Si le bouton gauche de la souris est relâché et que la tête n'est pas la tête du haut du personnage
        {
            rb.MovePosition((Vector2)playerPos.transform.position); // La tête revient à sa position de départ
            rb.MoveRotation(0);
            joint.enabled = true; // Réactivation du joint qui relie la tête au corps
            isAttacking = false; // Indique que la tête a fini d'attaquer
            isOnClick = false;
        }

        if (isAttacking) // Si le bouton gauche de la souris est enfoncé
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, rotationZ)); // Rotation de la tête en fonction de la direction de la souris
        }
        else
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, 10000 * Time.deltaTime));  // Si le bouton gauche de la souris n'est pas enfoncé, la tête revient à sa position
        }
    }
}