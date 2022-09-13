using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float damage;
    private IDamageable target;
    private ViewDetector viewDetector;
    private AttackController attackController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.GetComponentInParent<AttackController>();
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
        animator.speed = 1f * attackController.attackSpeed;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    
}
