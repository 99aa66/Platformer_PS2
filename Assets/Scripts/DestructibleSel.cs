using UnityEngine;

public class DestructibleSel : MonoBehaviour
{
    public GameObject destroyedVersion;
    public float destroyDelay = 1.5f;
    public GameObject selPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BossMama"))
        {
            GameObject brokenObject = Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(brokenObject, destroyDelay);
            Destroy(gameObject);

            GameObject sel = Instantiate(selPrefab, collision.transform.position, collision.transform.rotation); //faire apparaître à la pos de la Mama
            Rigidbody2D selRigidbody = sel.GetComponent<Rigidbody2D>();
            if (selRigidbody != null)
            {
                selRigidbody.velocity = Vector2.zero;
            }
        }
    }
}
