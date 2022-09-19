using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GiantWormStates
{

    public class BaseState : State<GiantWorm>
    {
        private bool isDie = false;
        public override void Enter(GiantWorm Owner)
        {
        }

        public override void Update(GiantWorm Owner)
        {

        }

        public override void Exit(GiantWorm Owner)
        {

        }

        public override void HandleStateChange(GiantWorm Owner)
        {
            CheckDie(Owner);
            CheckSkill(Owner);


        }
        public void CheckDie(GiantWorm Owner)
        {
            if (Owner.hpController.hp <= 0)
            {
                isDie = true;
                Owner.ChangeState(GiantWorm.State.Die);
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

        public void CheckSkill(GiantWorm Owner)
        {
            if (Owner.hpController.initMp > 0 && Owner.hpController.mp == Owner.hpController.initMp)
            {
                Owner.hpController.mp -= Owner.hpController.initMp;
                Owner.ChangeState(GiantWorm.State.Skill);
            }
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(GiantWorm Owner)
        {

        }

        public override void Update(GiantWorm Owner)
        {
            GameObject findtarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.findRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                findtarget = targets[0].gameObject;
                Owner.ChangeState(GiantWorm.State.Trace);
                return;
            }
            else
            {
                findtarget = null;
            }

        }

        public override void Exit(GiantWorm Owner)
        {

        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(GiantWorm Owner)
        {

        }

        public override void Update(GiantWorm Owner)
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
                attackTarget = attackTargets[0].gameObject;
                Owner.ChangeState(GiantWorm.State.Attack);
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
                traceTarget = targets[0].gameObject;
                //Owner.animator.SetBool("isRun", true);
            }
            else
            {
                traceTarget = null;
            }

            if (traceTarget == null)
            {
                Owner.ChangeState(GiantWorm.State.Idle);
                return;
            }

            Vector3 moveDir = traceTarget.transform.position - Owner.transform.position;
            Owner.characterController.Move(new Vector3(moveDir.x, Physics.gravity.y, moveDir.z).normalized * Time.deltaTime * Owner.moveSpeed);
            Owner.transform.LookAt(new Vector3(traceTarget.transform.position.x, Owner.transform.position.y, traceTarget.transform.position.z));



        }

        public override void Exit(GiantWorm Owner)
        {
           // Owner.animator.SetBool("isRun", false);
        }
    }

    public class AttackState : BaseState
    {
        private bool isAttackking;
        public override void Enter(GiantWorm Owner)
        {

        }

        public override void Update(GiantWorm Owner)
        {

            if (isAttackking == false)
            {
                Owner.StartCoroutine(AttackTime(Owner));
            }
        }

        public override void Exit(GiantWorm Owner)
        {
            Owner.animator.ResetTrigger("Attack");
        }

        IEnumerator AttackTime(GiantWorm Owner)
        {
            isAttackking = true;
            //int randomNum = Random.Range(1, 3);
            Owner.animator.SetTrigger("Attack");
            //Owner.animator.SetInteger("randomAttack", randomNum);
            yield return new WaitForSeconds(1f);
            Owner.ChangeState(GiantWorm.State.Idle);
            isAttackking = false;
        }
    }

    public class SkillState : BaseState
    {
        private bool isSkill;
        public override void Enter(GiantWorm Owner)
        {
            if (isSkill == false)
            {
                Owner.StartCoroutine(SkillRoutine(Owner));
            }
        }

        public override void Update(GiantWorm Owner)
        {


        }

        public override void Exit(GiantWorm Owner)
        {
            Owner.animator.ResetTrigger("Skill");
        }

        IEnumerator SkillRoutine(GiantWorm Owner)
        {
            isSkill = true;
            Owner.animator.SetTrigger("Skill");
            yield return new WaitForSeconds(3f);
            isSkill = false;
            Owner.ChangeState(GiantWorm.State.Idle);
        }
    }
    public class StunState : BaseState
    {
        public override void Enter(GiantWorm Owner)
        {

        }

        public override void Update(GiantWorm Owner)
        {


        }

        public override void Exit(GiantWorm Owner)
        {

        }
    }

    public class DieState : BaseState
    {
        private bool isDie = true;
        public override void Enter(GiantWorm Owner)
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

        public override void Update(GiantWorm Owner)
        {

        }

        public override void Exit(GiantWorm Owner)
        {

        }
    }
}