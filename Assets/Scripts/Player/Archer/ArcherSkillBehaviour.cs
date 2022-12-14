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
        arrowDir = new Vector3[9];

        arrowDir[0] = AngleToDir(archer.transform.eulerAngles.y - 120 * 0.5f);
        arrowDir[1] = AngleToDir(archer.transform.eulerAngles.y - 120 * 0.375f);
        arrowDir[2] = AngleToDir(archer.transform.eulerAngles.y - 120 * 0.25f);
        arrowDir[3] = AngleToDir(archer.transform.eulerAngles.y - 120 * 0.125f);
        arrowDir[4] = AngleToDir(archer.transform.eulerAngles.y);
        arrowDir[5] = AngleToDir(archer.transform.eulerAngles.y + 120 * 0.125f);
        arrowDir[6] = AngleToDir(archer.transform.eulerAngles.y + 120 * 0.25f);
        arrowDir[7] = AngleToDir(archer.transform.eulerAngles.y + 120 * 0.375f);
        arrowDir[8] = AngleToDir(archer.transform.eulerAngles.y + 120 * 0.5f);

        for (int i = 0; i < 9; i++)
        {
            ObjectPooling.poolDic["Arrow"].GetPool(archer.shotPoint.position, Quaternion.LookRotation(arrowDir[i]));

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

}
