using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCouteauEffect : MonoBehaviour
{
    Vector2 startPos;

    [Header("Sprite Renderer")]
    SpriteRenderer JambeD;
    SpriteRenderer PiedD;
    SpriteRenderer BrasD;
    SpriteRenderer MainD;
    SpriteRenderer Corps;
    SpriteRenderer BrasG;
    SpriteRenderer MainG;
    SpriteRenderer JambeG;
    SpriteRenderer PiedG;
    SpriteRenderer oeil;
    SpriteRenderer bouche;

    private void Awake()
    {
        JambeD = GetComponent<SpriteRenderer>();
        PiedD = GetComponent<SpriteRenderer>();
        BrasD = GetComponent<SpriteRenderer>();
        MainD = GetComponent<SpriteRenderer>();
        Corps = GetComponent<SpriteRenderer>();
        BrasG = GetComponent<SpriteRenderer>();
        MainG = GetComponent<SpriteRenderer>();
        JambeG = GetComponent<SpriteRenderer>();
        PiedG = GetComponent<SpriteRenderer>();
        oeil = GetComponent<SpriteRenderer>();
        bouche = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        JambeD.enabled = false;
        PiedD.enabled = false;
        BrasD.enabled = false;
        MainD.enabled = false;
        Corps.enabled = false;
        BrasG.enabled = false;
        MainG.enabled = false;
        JambeG.enabled = false;
        PiedG.enabled = false;
        oeil.enabled = false;
        bouche.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        JambeD.enabled = true;
        PiedD.enabled = true;
        BrasD.enabled = true;
        MainD.enabled = true;
        Corps.enabled = true;
        BrasG.enabled = true;
        MainG.enabled = true;
        JambeG.enabled = true;
        PiedG.enabled = true;
        oeil.enabled = true;
        bouche.enabled = true;
    }
}
