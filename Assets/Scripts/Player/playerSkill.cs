using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSkill : StateMachineBehaviour
{
    private NormalWarrior player;
    private Collider[] colliders;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponentInParent<NormalWarrior>();

        player.playerSkill.SetActive(true);

        colliders = Physics.OverlapSphere(player.transform.position + Vector3.up, 6, 1<<6);

        foreach(var collider in colliders)
        {
            collider.GetComponent<Monster>().HitDamage(WeaponManager.Instance.minDamage * 2);
        }
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.playerSkill.SetActive(false);
        player.ChangeState(NormalWarrior.State.Idle);
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
