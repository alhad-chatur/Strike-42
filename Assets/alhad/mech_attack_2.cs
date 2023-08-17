using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mech_attack_2 : StateMachineBehaviour
{
    Transform player;
    Transform eye;

    int counter = 0;
    int currgun = 2;
    void gunai()
    {
        float temp = Random.Range(0, 100);
        if (temp <= 70)
            currgun = 2;
        else
            currgun = 1;
    }
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        eye = animator.transform.Find("eye").transform;
        currgun = 2;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<Enemy_attack>().currenttime() == 1)
            gunai();

        if (currgun == 1)
        {
            animator.SetInteger("action", 3);
        }

        animator.transform.LookAt(player);

        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (counter > 15)
        {
            animator.SetInteger("action", 2);
        }

        //Vector3 forward = new Vector3(eye.forward.x, 0, eye.forward.z);
        Vector3 forward = player.position - eye.position;

        RaycastHit hit;

        Physics.SphereCast(eye.position, 0.5f * 0.02f, forward, out hit);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
                counter = 0;
            else if (hit.distance > Vector3.Distance(player.transform.position, eye.position) * 1.1)
                counter = 0;
            else
                counter++;
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
