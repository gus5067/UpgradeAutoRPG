using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSkillBehaviour : StateMachineBehaviour
{
    private Archer archer;
    private Vector3[] arrowDir;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        archer = animator.GetComponentInParent<Archer>();
        arrowDir = new Vector3[5];

        arrowDir[0] = AngleToDir(archer.transform.eulerAngles.y - 120 * 0.5f);
        arrowDir[1] = AngleToDir(archer.transform.eulerAngles.y - 120 * 0.25f);
        arrowDir[2] = AngleToDir(archer.transform.eulerAngles.y);
        arrowDir[3] = AngleToDir(archer.transform.eulerAngles.y + 120 * 0.25f);
        arrowDir[4] = AngleToDir(archer.transform.eulerAngles.y + 120 * 0.5f);

        for (int i = 0; i < 5; i++)
        {
            var Arrow = ObjectPooling.GetObject();
            Arrow.transform.position = archer.shotPoint.position;
            Arrow.Shoot(arrowDir[i]);
        }
    }
    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
       
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        archer.ChangeState(Archer.State.Idle);
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
