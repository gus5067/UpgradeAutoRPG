using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttackBehaviour : StateMachineBehaviour
{

    private Wizard wizard;
    private ViewDetector viewDetector;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wizard = animator.GetComponentInParent<Wizard>();

        wizard.hpController.OnChangeMp(1);
        viewDetector = wizard.GetComponent<ViewDetector>();

        viewDetector.FindTarget();
        wizard.FireAttack(viewDetector.target);
        
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
