using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLid : MonoBehaviour
{
    [SerializeField] GameObject playerPos;
    [SerializeField] float speed;
    public Transform[] waypoints;
    private Transform target;
    private int destPoint = 0;
    bool onLid = false;

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
        }

        if (onLid)
        {
            playerPos.transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            onLid = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            onLid = false;
        }
    }
}

