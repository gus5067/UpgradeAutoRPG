using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShield : StateMachineBehaviour
{
    private Wizard wizard;
    private GameObject obj;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wizard = animator.GetComponentInParent<Wizard>();
        wizard.gameObject.layer = 8;

        obj = Instantiate(wizard.skills[0], wizard.transform.position, Quaternion.identity);

        Debug.Log("Ω√¿€ : " + obj);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wizard.gameObject.layer = 7;
        wizard.ChangeState(Player.State.Idle);
        Debug.Log("≥° : " + obj);
        Destroy(obj);
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
