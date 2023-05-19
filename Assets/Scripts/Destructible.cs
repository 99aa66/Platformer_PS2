using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public float destroyDelay = 1.5f;
    public int damageOnCollision = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isAttacking = Head1.isAttacking || (collision.gameObject.GetComponent<Head>()?.isAttacking ?? false);

        if (collision.gameObject.CompareTag("Player") && isAttacking)
        {

            PlayerHealth.instance.TakeDamage(damageOnCollision);
            GameObject brokenObject = Instantiate(destroyedVersion,transform.position,transform.rotation);
            Destroy(brokenObject, destroyDelay);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Cafetière")
        {
            GameObject brokenObject = Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(brokenObject, destroyDelay);
            Destroy(gameObject);
        }
    }
}
