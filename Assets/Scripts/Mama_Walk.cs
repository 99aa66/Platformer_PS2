using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mama_Walk : StateMachineBehaviour
{
    [SerializeField] float speed = 3f;

    Transform player;
    Rigidbody2D rb;
    BossMama boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerController>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BossMama>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
}
