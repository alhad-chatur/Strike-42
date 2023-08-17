using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mech_chase : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Transform eye;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //eye = GameObject.FindGameObjectWithTag("eye").transform;
        eye = animator.transform.Find("eye").transform; 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        Vector3 forward = player.position - eye.position;
        RaycastHit hit;

        Physics.SphereCast(eye.position, 0.5f * 0.02f, forward, out hit);

        if ((distance < 10|| animator.GetComponent<Enemy_attack>().hurt==true) && hit.collider.gameObject.tag == "Player")
        {
            animator.SetInteger("action", 3);
        }
        if (distance > 30 && animator.GetComponent<Enemy_attack>().hurt==false)
            animator.SetInteger("action", 1);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
