using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Mouvement Particle")]
    [SerializeField] ParticleSystem mouvementParticle;
    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity; //vitesse du joueur à atteindre pour former de la poussière
    [Range(0, 1f)]
    [SerializeField] float dustFormationPeriod;
    float counter;
    bool isOnGround;

    [Header("Fall Particle")]
    [SerializeField] ParticleSystem fallParticle;

    [SerializeField] Rigidbody2D playerRb;
    private void Update()
    {
        counter += Time.deltaTime;

        if(isOnGround && Mathf.Abs(playerRb.velocity.x)> occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                mouvementParticle.Play();
                counter = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            fallParticle.Play();
            isOnGround= true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround= false;
        }
    }
}
