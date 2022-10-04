using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    [SerializeField]
    public int damage;
    private IDamageable target;
    private ViewDetector viewDetector;
    private Monster monster;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponentInParent<Monster>();
        animator.GetComponentInParent<monsterHpController>().OnChangeMp(1);
        animator.speed = animator.GetComponentInParent<Monster>().attackTime + 1f;
        viewDetector = animator.GetComponentInParent<ViewDetector>();
        viewDetector.FindTarget();
        if (viewDetector.target != null)
        {
            monster.gameObject.transform.LookAt(viewDetector.target.transform.position);
            target = viewDetector.target.GetComponent<IDamageable>();
            if (target != null)
            {
                monster.MonsterAttackSound();
                target.HitDamage(damage);
            }
        }
    }
}
