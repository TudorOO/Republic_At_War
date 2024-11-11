using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelEnemyRunB : StateMachineBehaviour
{

    Transform player;
    Rigidbody2D rb;


    private bool isAttacking = false;
    private DuelEnemyController enemyController;

    private int rand;

    public float speed = 2.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>(); 
        enemyController = animator.GetComponent<DuelEnemyController>();
        isAttacking = false;

    }



    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        if(Mathf.Abs(player.position.x - rb.position.x) >= 4f){
            rb.MovePosition(newPos);
        }else{
            rand = Random.Range(0, 50);
            if(rand == 0 && isAttacking == false){
                enemyController.attacker = true;
                isAttacking = true;
            }else{
                rb.MovePosition(newPos);
            }
        }


        if(player.position.y - rb.position.y > 1f){
            rand = Random.Range(0, 50);
            if(rand == 0){
                if(player.position.x - rb.position.x < 0){
                    enemyController.Jump(false);
                }else{
                    enemyController.Jump(true);
                }
                animator.SetBool("isJumping", true);
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
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
