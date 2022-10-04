using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishManStates
{
    public class BaseState : State<FishMan>
    {
        private bool isDie = false;
        public override void Enter(FishMan Owner)
        {
        }

        public override void Update(FishMan Owner)
        {

        }

        public override void Exit(FishMan Owner)
        {

        }

        public override void HandleStateChange(FishMan Owner)
        {
            CheckDie(Owner);
            if(!isDie)
            {
                CheckSkill(Owner);
            }
          


        }
        public void CheckDie(FishMan Owner)
        {
            if (Owner.hpController.hp <= 0)
            {
                isDie = true;
                Owner.ChangeState(FishMan.State.Die);
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

        public void CheckSkill(FishMan Owner)
        {
            if (Owner.hpController.initMp > 0 && Owner.hpController.mp >= Owner.hpController.initMp)
            {
                Owner.hpController.mp -= Owner.hpController.initMp;
                Owner.ChangeState(FishMan.State.Skill);
            }
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(FishMan Owner)
        {

        }

        public override void Update(FishMan Owner)
        {
            GameObject findtarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.findRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                findtarget = Owner.ChangeTarget(targets, true).gameObject;
                Owner.ChangeState(FishMan.State.Trace);
                return;
            }
            else
            {
                findtarget = null;
            }
          
        }

        public override void Exit(FishMan Owner)
        {

        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(FishMan Owner)
        {

        }

        public override void Update(FishMan Owner)
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
                Owner.ChangeState(FishMan.State.Attack);
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
                Owner.ChangeState(FishMan.State.Idle);
                return;
            }

            Vector3 moveDir = traceTarget.transform.position - Owner.transform.position;
            Owner.characterController.Move(new Vector3(moveDir.x, Physics.gravity.y, moveDir.z).normalized * Time.deltaTime * Owner.moveSpeed);
            Owner.transform.LookAt(new Vector3(traceTarget.transform.position.x, Owner.transform.position.y, traceTarget.transform.position.z));


        }

        public override void Exit(FishMan Owner)
        {
            Owner.animator.SetBool("isRun", false);
        }
    }

    public class AttackState : BaseState
    {
        private bool isAttackking;
        public override void Enter(FishMan Owner)
        {
        }

        public override void Update(FishMan Owner)
        {

            if (isAttackking == false)
            {
                Owner.StartCoroutine(AttackTime(Owner));
            }

           
        }

        public override void Exit(FishMan Owner)
        {
            Owner.animator.ResetTrigger("Attack");
        }

        IEnumerator AttackTime(FishMan Owner)
        {
            isAttackking = true;
            int randomNum = Random.Range(1, 3);
            Owner.animator.SetTrigger("Attack");
            Owner.animator.SetInteger("randomAttack", randomNum);
            yield return new WaitForSeconds(1.5f - Owner.attackTime);
            Owner.ChangeState(FishMan.State.Idle);
            isAttackking = false;
        }
    }

    public class SkillState : BaseState
    {
        private bool isSkill;
        public override void Enter(FishMan Owner)
        {
            if (isSkill == false)
            {
                Owner.StartCoroutine(SkillRoutine(Owner));
            }
        }

        public override void Update(FishMan Owner)
        {
           

        }

        public override void Exit(FishMan Owner)
        {
            Owner.animator.ResetTrigger("Skill");
        }

        IEnumerator SkillRoutine(FishMan Owner)
        {
            isSkill = true;
            Owner.animator.SetTrigger("Skill");
            yield return new WaitForSeconds(2f);
            isSkill = false;
            //Owner.ChangeState(Grunt.State.Idle);
        }
    }
    public class DodgeState : BaseState
    {
        private bool isDodge = false;
        public override void Enter(FishMan Owner)
        {
            if(isDodge == false)
            {
                Owner.StartCoroutine(DodgeRoutine(Owner));
            }
            else
            {
                Owner.ChangeState(FishMan.State.Idle);
            }
        }

        public override void Update(FishMan Owner)
        {
           

        }

        public override void Exit(FishMan Owner)
        {
            Owner.animator.ResetTrigger("Dodge");
        }

        IEnumerator DodgeRoutine(FishMan Owner)
        {
            isDodge = true;
            Owner.animator.SetTrigger("Dodge");
            yield return new WaitForSeconds(0.5f);
            isDodge = false;
            Owner.ChangeState(FishMan.State.Idle);
        }
    }

    public class DieState : BaseState
    {
        private bool isDie = true;
        public override void Enter(FishMan Owner)
        {
            if (isDie == true)
            {
                isDie = false;
                Owner.animator.SetTrigger("Die");
                GameManager.Instance.AddDictionary("어인잡은 횟수");
                Owner.DropItem();
                Owner.DieCount();
                Owner.characterController.enabled = false;
                Owner.Die(1f);
            }


        }

        public override void Update(FishMan Owner)
        {

        }

        public override void Exit(FishMan Owner)
        {

        }
    }
}