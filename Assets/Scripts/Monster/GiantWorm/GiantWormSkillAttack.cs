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
        controller = animator.GetComponentInParent<CharacterController>();
        worm = animator.GetComponentInParent<GiantWorm>();

        worm.gameObject.layer = 6;

        GameObject attackTarget;
        Collider[] attackTargets = Physics.OverlapSphere(worm.gameObject.transform.position, worm.attackRange, worm.targetLayerMask);
        if (attackTargets.Length > 0)
        {
            worm.GiantWormSkill(false);
            attackTarget = worm.ChangeTarget(attackTargets, true).gameObject;
            attackTarget.GetComponent<Transform>().position = attackTarget.transform.position + Vector3.up * 1.5f;
            IStunable target = attackTarget.GetComponent<IStunable>();
            target?.Stunned();

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
        worm.skillTarget = null;
        worm.ChangeState(GiantWorm.State.Idle);
    }

}
