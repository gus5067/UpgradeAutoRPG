using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SerpentStates
{
    public class BaseState : State<Serpent>
    {
        private bool isDie = false;
        public override void Enter(Serpent Owner)
        {
        }

        public override void Update(Serpent Owner)
        {

        }

        public override void Exit(Serpent Owner)
        {

        }

        public override void HandleStateChange(Serpent Owner)
        {
            CheckDie(Owner);
            CheckSkill(Owner);


        }
        public void CheckDie(Serpent Owner)
        {
            if (Owner.hpController.hp <= 0)
            {
                isDie = true;
                Owner.ChangeState(Serpent.State.Die);
            }
            else
            {
                return;
            }

            if (!isDie)
            {
                if (!Owner.isGround)
                {
                    Owner.characterController.Move(new Vector3(0, Physics.gravity.y, 0).normalized * Time.deltaTime);
                }
            }
        }

        public void CheckSkill(Serpent Owner)
        {
            if (Owner.hpController.initMp > 0 && Owner.hpController.mp >= Owner.hpController.initMp)
            {
                Owner.hpController.mp -= Owner.hpController.initMp;
                Owner.ChangeState(Serpent.State.Skill);
            }
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(Serpent Owner)
        {

        }

        public override void Update(Serpent Owner)
        {
            GameObject findtarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.findRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                findtarget = Owner.ChangeTarget(targets).gameObject;
                Owner.ChangeState(Serpent.State.Trace);
                return;
            }
            else
            {
                findtarget = null;
            }

        }

        public override void Exit(Serpent Owner)
        {

        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(Serpent Owner)
        {

        }

        public override void Update(Serpent Owner)
        {
            if (Owner.characterController == null)
            {
                return;
            }
            //몬스터 공격
            GameObject attackTarget;
            Collider[] attackTargets = Physics.OverlapSphere(Owner.transform.position, Owner.rangeAttackRange, Owner.targetLayerMask);
            if (attackTargets.Length > 0)
            {
                attackTarget = Owner.ChangeTarget(attackTargets).gameObject;
                Owner.ChangeState(Serpent.State.Attack);
                return;
            }
            else
            {
                attackTarget = null;
            }
            //몬스터 추적
            GameObject traceTarget = null;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.findRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                traceTarget = Owner.ChangeTarget(targets).gameObject;
                Owner.animator.SetBool("isRun", true);
            }
            else
            {
                traceTarget = null;
            }

            if (traceTarget == null)
            {
                Owner.ChangeState(Serpent.State.Idle);
                return;
            }

            Vector3 moveDir = traceTarget.transform.position - Owner.transform.position;
            Owner.characterController.Move(new Vector3(moveDir.x, Physics.gravity.y, moveDir.z).normalized * Time.deltaTime * Owner.moveSpeed);
            Owner.transform.LookAt(new Vector3(traceTarget.transform.position.x, Owner.transform.position.y, traceTarget.transform.position.z));



        }

        public override void Exit(Serpent Owner)
        {
            Owner.animator.SetBool("isRun", false);
        }
    }

    public class AttackState : BaseState
    {
        private bool isAttackking;
        private bool isMeleeRange;
        public override void Enter(Serpent Owner)
        {
            GameObject attackTarget;
            Collider[] attackTargets = Physics.OverlapSphere(Owner.transform.position, Owner.attackRange, Owner.targetLayerMask);
            if (attackTargets.Length > 0)
            {
                attackTarget = Owner.ChangeTarget(attackTargets).gameObject;
                isMeleeRange = true;
                Owner.animator.SetBool("isMelee", true);
                return;
            }
            else
            {
                isMeleeRange=false;
                Owner.animator.SetBool("isMelee", false);
            }
        }

        public override void Update(Serpent Owner)
        {

            if (isAttackking == false)
            {
                Owner.StartCoroutine(AttackTime(Owner));
            }
        }

        public override void Exit(Serpent Owner)
        {
            Owner.animator.ResetTrigger("Attack");
        }

        IEnumerator AttackTime(Serpent Owner)
        {
            isAttackking = true;
            Owner.animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1.5f - Owner.attackTime);
            Owner.ChangeState(Serpent.State.Idle);
            isAttackking = false;
        }
    }

    public class SkillState : BaseState
    {
        private bool isSkill;
        public override void Enter(Serpent Owner)
        {
            if (isSkill == false)
            {
                Owner.StartCoroutine(SkillRoutine(Owner));
            }
        }

        public override void Update(Serpent Owner)
        {


        }

        public override void Exit(Serpent Owner)
        {
            Owner.animator.ResetTrigger("Skill");
        }

        IEnumerator SkillRoutine(Serpent Owner)
        {
            isSkill = true;
            Owner.animator.SetTrigger("Skill");
            yield return new WaitForSeconds(2f);
            isSkill = false;
            //Owner.ChangeState(Grunt.State.Idle);
        }
    }
    public class StunState : BaseState
    {
        public override void Enter(Serpent Owner)
        {

        }

        public override void Update(Serpent Owner)
        {


        }

        public override void Exit(Serpent Owner)
        {

        }
    }

    public class DieState : BaseState
    {
        private bool isDie = true;
        public override void Enter(Serpent Owner)
        {
            if (isDie == true)
            {
                isDie = false;
                Owner.animator.SetTrigger("Die");
                Owner.DropItem();
                Owner.DieCount();
                Owner.characterController.enabled = false;
                Owner.Die(1.5f);
            }


        }

        public override void Update(Serpent Owner)
        {

        }

        public override void Exit(Serpent Owner)
        {

        }
    }
}