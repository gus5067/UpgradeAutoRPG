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
        controller = animator.GetComponent<CharacterController>();
        worm = animator.GetComponent<GiantWorm>();

        controller.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.enabled = false;
        GameObject traceTarget = null;
        Collider[] targets = Physics.OverlapSphere(animator.gameObject.transform.position, worm.findRange, worm.targetLayerMask);
        if (targets.Length > 0)
        {
            traceTarget = targets[0].gameObject;
            Debug.Log(traceTarget.name);
        }
        Vector3 moveDir = traceTarget.transform.position - worm.gameObject.transform.position;
        Debug.Log(moveDir);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, traceTarget.transform.position, worm.moveSpeed * Time.deltaTime);
        animator.gameObject.transform.LookAt(new Vector3(traceTarget.transform.position.x, animator.gameObject.transform.position.y, traceTarget.transform.position.z));

        if(moveDir.magnitude < 1f)
        {
            animator.SetTrigger("SkillOn");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
