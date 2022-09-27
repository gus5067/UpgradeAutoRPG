using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardExplosion : StateMachineBehaviour
{
    private Wizard wizard;
    private ViewDetector viewDetector;
    private GameObject obj;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wizard = animator.GetComponentInParent<Wizard>();
        viewDetector = wizard.GetComponent<ViewDetector>();

        viewDetector.FindTarget();

        if (viewDetector.target != null)
        {
            obj = Instantiate(wizard.skills[4], viewDetector.target.transform);
        }


        Collider[] targets = Physics.OverlapSphere(obj.transform.position, 5f, 1 << 6);
        foreach(var target in targets)
        {
            Monster monster = target.GetComponent<Monster>();
            monster?.HitDamage(WeaponManager.Instance.maxDamage * 2);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(obj);
        wizard.ChangeState(Player.State.Idle);
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
