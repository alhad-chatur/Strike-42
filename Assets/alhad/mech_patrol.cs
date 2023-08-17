using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mech_patrol : StateMachineBehaviour
{
   
    Transform player;
    float time;
    List<Transform> check_points = new List<Transform>();
    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        time = 0;
        Transform obj = GameObject.FindGameObjectWithTag("Active").transform;


        foreach (Transform t in obj)
        {
            check_points.Add(t);
        }

        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(check_points[0].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, agent.pathEndPosition) <= 2f)
        {
            agent.SetDestination(check_points[Random.Range(0, check_points.Count)].position);
        }
       
        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance < 15)
        {
            animator.SetInteger("action", 2);
        }
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
