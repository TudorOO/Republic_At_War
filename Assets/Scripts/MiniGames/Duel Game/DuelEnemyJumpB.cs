using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelEnemyJumpB : StateMachineBehaviour
{

    private Rigidbody2D rb;
    private DuelEnemyController enemyController;
    private Transform player;
    private int rand;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>(); 
        enemyController = animator.GetComponent<DuelEnemyController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(rb.velocity.y < 0){
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
        }

        if(Mathf.Abs(player.position.x - rb.position.x) >= 4f){

        }else{
            rand = Random.Range(0, 50);
            if(rand == 0){
                enemyController.attacker = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
