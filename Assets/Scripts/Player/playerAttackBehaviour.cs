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
    private TrailRenderer trailRenderer;
    private Player player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponentInParent<Player>();
        animator.GetComponentInParent<HpController>().OnChangeMp(1);
        trailRenderer = animator.GetComponentInParent<NormalWarrior>().trailRenderer;
        trailRenderer.enabled = true;
        attackController = animator.GetComponentInParent<AttackController>();
        viewDetector = animator.GetComponentInParent<ViewDetector>();
        viewDetector.FindTarget();
        if (viewDetector.target != null)
        {
            target = viewDetector.target.GetComponent<IDamageable>();
            if (target != null)
            {
                player.AttackSound();
                damage = Random.Range(attackController.minDamage, attackController.maxDamage + 1);
                switch (WeaponManager.Instance.WeaponStateNum)
                {
                    case 0:
                        target.HitDamage(damage);
                        break;
                    case 1:
                        target.HitDamage(attackController.minDamage);
                        animator.GetComponentInParent<IDamageable>().HitDamage(-attackController.minDamage / 8);
                        if(animator.GetComponentInParent<HpController>().hp >= animator.GetComponentInParent<HpController>().initHp)
                        {
                            animator.GetComponentInParent<HpController>().hp = animator.GetComponentInParent<HpController>().initHp;
                        }
                        break;
                    case 2:
                        target.HitDamage(attackController.maxDamage);
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
        trailRenderer.enabled = false;
    }
}
