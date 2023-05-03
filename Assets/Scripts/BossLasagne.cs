using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLasagne : MonoBehaviour
{
    public Vector3 playerPos;

    public bool isFlipped = false;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > playerPos.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerPos.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
