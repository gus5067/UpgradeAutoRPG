using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSkillBehaviour : StateMachineBehaviour
{
    private Wizard wizard;
    private ViewDetector viewDetector;
    private GameObject skill;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wizard = animator.GetComponentInParent<Wizard>();
        viewDetector = wizard.GetComponent<ViewDetector>();
        skill = Instantiate(wizard.skills[2], wizard.transform);
        skill.transform.SetParent(wizard.transform);
        foreach(var target in viewDetector.FindTargets(8.8f, 42f))
        {
            Monster monster = target.GetComponent<Monster>();
            monster?.HitDamage(20);
            monster.hitState = Monster.HitState.Forzen;
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
        wizard.ChangeState(Player.State.Idle);
        Destroy(skill);
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
