using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWormSkillAttack : StateMachineBehaviour
{
    private CharacterController controller;
    private GiantWorm worm;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponent<CharacterController>();
        worm = animator.GetComponent<GiantWorm>();

        controller.enabled = true;

        GameObject attackTarget;
        Collider[] attackTargets = Physics.OverlapSphere(worm.gameObject.transform.position, worm.attackRange, worm.targetLayerMask);
        if (attackTargets.Length > 0)
        {
            attackTarget = attackTargets[0].gameObject;
            attackTarget.GetComponent<NormalWarrior>().ChangeState(NormalWarrior.State.Stun);
            return;
        }
        else
        {
            attackTarget = null;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
