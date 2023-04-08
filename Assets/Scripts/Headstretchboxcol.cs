using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headstretchboxcol : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private HingeJoint2D joint;
    private BoxCollider2D boxCollider;

    [SerializeField] float distMax = 3;
    [SerializeField] GameObject playerPos;

    [SerializeField] BoxCollider2D selectedBoxCollider;
    private Vector2 originalBoxColliderSize;
    private Vector2 originalBoxColliderOffset;
    private bool isDragging = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        joint = GetComponent<HingeJoint2D>();
        originalBoxColliderSize = selectedBoxCollider.size;
        originalBoxColliderOffset = selectedBoxCollider.offset;
    }

    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 difference = (mousePos - (Vector2)playerPos.transform.position).normalized;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Debug.DrawRay((Vector2)playerPos.transform.position, difference * distMax, Color.blue);

        if (Input.GetButtonDown("Clic gauche") && rb.gameObject.name != "Top_Head")
        {
            joint.enabled = false;
            isDragging = true;
        }
        if (Input.GetButtonUp("Clic gauche") && rb.gameObject.name != "Top_Head")
        {
            rb.MovePosition((Vector2)playerPos.transform.position);
            rb.MoveRotation(0);
            joint.enabled = true;
            isDragging = false;
            selectedBoxCollider.offset = originalBoxColliderOffset;
        }

        if (Input.GetButton("Clic gauche"))
        {
            float dist = Vector2.Distance(playerPos.transform.position, mousePos);
            float newWidth = Mathf.Clamp(dist, 0, distMax);
            Vector2 newBoxColliderSize = new Vector2(newWidth, originalBoxColliderSize.y);
            selectedBoxCollider.size = newBoxColliderSize;
            Vector2 newPosition = (Vector2)playerPos.transform.position + difference * newWidth / 2;
            selectedBoxCollider.offset = originalBoxColliderOffset + (newPosition - (Vector2)transform.position);
            rb.MoveRotation(rotationZ);

            if (rb.gameObject.name == "Top_Head")
            {
                rb.MovePosition((Vector2)playerPos.transform.position + difference * distMax);
            }
        }
        else
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, 90f, 300 * Time.deltaTime));
        }
    }
}