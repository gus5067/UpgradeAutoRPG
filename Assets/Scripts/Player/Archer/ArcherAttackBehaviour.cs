using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackBehaviour : StateMachineBehaviour
{
    private Archer archer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        archer = animator.GetComponentInParent<Archer>();
        archer.AttackSound();
        archer.hpController.OnChangeMp(1);
        ObjectPooling.poolDic["Arrow"].GetPool(archer.shotPoint.position, archer.transform.rotation);

    }
}
