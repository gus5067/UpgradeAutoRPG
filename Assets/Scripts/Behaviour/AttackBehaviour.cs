using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    [SerializeField]
    public int damage;
    private IDamageable target;
    private ViewDetector viewDetector;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<monsterHpController>().OnChangeMp(1);
        animator.speed = animator.GetComponentInParent<Monster>().attackTime + 1f;
        viewDetector = animator.GetComponentInParent<ViewDetector>();
        viewDetector.FindTarget();
        if (viewDetector.target != null)
        {
            target = viewDetector.target.GetComponent<IDamageable>();
            if (target != null)
            {
                target.HitDamage(damage);
            }
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    
}
