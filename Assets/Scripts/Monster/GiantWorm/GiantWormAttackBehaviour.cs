using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWormAttackBehaviour : StateMachineBehaviour
{
    private GiantWorm monster;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponentInParent<GiantWorm>();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster.MonsterAttackSound();
        ObjectPooling.poolDic["WormShot"].GetPool(monster.ShotPoint.position, monster.transform.rotation);
    }
}
