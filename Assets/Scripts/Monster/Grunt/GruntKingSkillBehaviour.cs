using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntKingSkillBehaviour : StateMachineBehaviour
{
    private GruntKing monster;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponentInParent<GruntKing>();
        monster.Heal();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
