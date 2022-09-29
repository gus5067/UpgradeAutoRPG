using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWormSkill : StateMachineBehaviour
{
    private CharacterController controller;
    private GiantWorm worm;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        
        controller = animator.GetComponentInParent<CharacterController>();
        worm = animator.GetComponentInParent<GiantWorm>();
        worm.GiantWormSkill(true);
        worm.gameObject.layer = 8;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject traceTarget = null;
        Collider[] targets = Physics.OverlapSphere(animator.gameObject.transform.position, worm.findRange, worm.targetLayerMask);
        if (targets.Length > 0 && worm.skillTarget == null)
        {
            traceTarget = worm.ChangeTarget(targets, false).gameObject;
            worm.skillTarget = traceTarget;
            Debug.Log(traceTarget.name);
        }
        Vector3 moveDir = worm.skillTarget.transform.position - worm.gameObject.transform.position;
        controller.Move(new Vector3(moveDir.x, Physics.gravity.y, moveDir.z).normalized * Time.deltaTime * worm.moveSpeed * 5f);
        worm.gameObject.transform.LookAt(new Vector3(worm.skillTarget.transform.position.x, worm.gameObject.transform.position.y, worm.skillTarget.transform.position.z));

        if(moveDir.magnitude < 1.5f)
        {
            animator.SetTrigger("SkillOn");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
