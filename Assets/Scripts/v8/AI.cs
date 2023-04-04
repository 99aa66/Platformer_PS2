using UnityEngine;
using System;

public class AI : MonoBehaviour
{
    public float speed = 5.0f;
    public float attackRange = 1.0f;
    public float detectionRange = 10.0f;

    public GameObject target;
    private bool isChasing = false;

    private Vector3 direction;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        direction = Vector3.right;
    }

    private void Update()
    {
        if (!isChasing)
        {
            // Move in current direction
            transform.position += direction * speed * Time.deltaTime;

            // Check if reached end of platform
            if (Mathf.Abs(transform.position.x - startPosition.x) >= 5)
            {
                // Turn around and move in opposite direction
                direction = -direction;
            }
        }
        else if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= attackRange)
            {
                Debug.Log("Attacking target!");
            }
            else
            {
                direction = (target.transform.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetTarget(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopChasing();
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        isChasing = true;
    }

    public void StopChasing()
    {
        target = null;
        isChasing = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
