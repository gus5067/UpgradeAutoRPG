using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnubisStates
{

    public class BaseState : State<Anubis>
    {
        private bool isDie = false;
        public override void Enter(Anubis Owner)
        {

        }

        public override void Update(Anubis Owner)
        {

        }

        public override void Exit(Anubis Owner)
        {

        }

        public override void HandleStateChange(Anubis Owner)
        {
            CheckDie(Owner);
            CheckSkill(Owner);


        }
        public void CheckDie(Anubis Owner)
        {
            if (Owner.hpController.hp <= 0)
            {
                isDie = true;
                Owner.ChangeState(Anubis.State.Die);
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

        public void CheckSkill(Anubis Owner)
        {
            if (Owner.hpController.initMp > 0 && Owner.hpController.mp >= Owner.hpController.initMp)
            {
                Owner.hpController.mp -= Owner.hpController.initMp;
                Owner.ChangeState(Anubis.State.Skill);
            }
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(Anubis Owner)
        {

        }

        public override void Update(Anubis Owner)
        {
            GameObject findtarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.findRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                findtarget = Owner.ChangeTarget(targets, true).gameObject;
                Owner.ChangeState(Anubis.State.Trace);
                return;
            }
            else
            {
                findtarget = null;
            }

        }

        public override void Exit(Anubis Owner)
        {

        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(Anubis Owner)
        {
        }

        public override void Update(Anubis Owner)
        {
            if (Owner.characterController == null)
            {
                return;
            }
            //몬스터 공격
            GameObject attackTarget;
            Collider[] attackTargets = Physics.OverlapSphere(Owner.transform.position, Owner.attackRange, Owner.targetLayerMask);
            if (attackTargets.Length > 0)
            {
                attackTarget = Owner.ChangeTarget(attackTargets, true).gameObject;
                Owner.ChangeState(Anubis.State.Attack);
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
                traceTarget = Owner.ChangeTarget(targets, true).gameObject;
                Owner.animator.SetBool("isRun", true);
            }
            else
            {
                traceTarget = null;
            }

            if (traceTarget == null)
            {
                Owner.ChangeState(Anubis.State.Idle);
                return;
            }

            Vector3 moveDir = traceTarget.transform.position - Owner.transform.position;
            Owner.characterController.Move(new Vector3(moveDir.x, Physics.gravity.y, moveDir.z).normalized * Time.deltaTime * Owner.moveSpeed);
            Owner.transform.LookAt(new Vector3(traceTarget.transform.position.x, Owner.transform.position.y, traceTarget.transform.position.z));



        }

        public override void Exit(Anubis Owner)
        {
            Owner.animator.SetBool("isRun", false);
        }
    }

    public class AttackState : BaseState
    {
        private bool isAttackking;
        public override void Enter(Anubis Owner)
        {
            GameObject attackTarget;
            Collider[] attackTargets = Physics.OverlapSphere(Owner.transform.position, Owner.attackRange, Owner.targetLayerMask);
            if (attackTargets.Length > 0)
            {
                attackTarget = Owner.ChangeTarget(attackTargets, true).gameObject;
                Owner.transform.LookAt(new Vector3(attackTarget.transform.position.x, Owner.transform.position.y, attackTarget.transform.position.z));
                return;

            }
            else
            {
                attackTarget = null;
            }
        }
        public override void Update(Anubis Owner)
        {

            if (isAttackking == false)
            {
                Owner.StartCoroutine(AttackTime(Owner));
            }
        }

        public override void Exit(Anubis Owner)
        {
            Owner.animator.ResetTrigger("Attack");
        }

        IEnumerator AttackTime(Anubis Owner)
        {
            isAttackking = true;
            int randomNum = Random.Range(1, 3);
            Owner.animator.SetTrigger("Attack");
            Owner.animator.SetInteger("randomAttack", randomNum);
            yield return new WaitForSeconds(1.2f);
            Owner.ChangeState(Anubis.State.Idle);
            isAttackking = false;
        }
    }

    public class SkillState : BaseState
    {
        private bool isSkill;
        public override void Enter(Anubis Owner)
        {
            if (isSkill == false)
            {
                Owner.StartCoroutine(SkillRoutine(Owner));
            }
        }

        public override void Update(Anubis Owner)
        {


        }

        public override void Exit(Anubis Owner)
        {
            Owner.animator.ResetTrigger("Skill");
        }

        IEnumerator SkillRoutine(Anubis Owner)
        {
            isSkill = true;
            Owner.animator.SetTrigger("Skill");
            yield return new WaitForSeconds(3f);
            isSkill = false;
        }
    }
    public class StunState : BaseState
    {
        public override void Enter(Anubis Owner)
        {

        }

        public override void Update(Anubis Owner)
        {


        }

        public override void Exit(Anubis Owner)
        {

        }
    }

    public class DieState : BaseState
    {
        private bool isDie = true;
        public override void Enter(Anubis Owner)
        {
            if (isDie == true)
            {
                isDie = false;
                Owner.animator.SetTrigger("Die");
                Owner.DropItem();
                Owner.DieCount();
                Owner.characterController.enabled = false;
                Owner.Die(1f);
            }


        }

        public override void Update(Anubis Owner)
        {

        }

        public override void Exit(Anubis Owner)
        {

        }
    }
}
