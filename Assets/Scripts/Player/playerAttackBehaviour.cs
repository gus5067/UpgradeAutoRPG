using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private IDamageable target;
    private ViewDetector viewDetector;
    private AttackController attackController;
    private int damage;
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
                damage = Random.Range(attackController.minDamage, attackController.maxDamage + 1);
                switch (WeaponManager.Instance.WeaponStateNum)
                {
                    case 0:
                        target.HitDamage(damage);
                        break;
                    case 1:
                        target.HitDamage(damage);
                        animator.GetComponentInParent<IDamageable>().HitDamage(-damage/4);
                        if(animator.GetComponentInParent<HpController>().hp >= animator.GetComponentInParent<HpController>().initHp)
                        {
                            animator.GetComponentInParent<HpController>().hp = animator.GetComponentInParent<HpController>().initHp;
                        }
                        break;
                    case 2:
                        target.HitDamage(damage * 2);
                        break;
                }
                
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
