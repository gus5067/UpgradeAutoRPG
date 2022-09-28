using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardStates
{
    public class BaseState : State<Wizard>
    {
        private bool isDie = false;
        public override void Enter(Wizard Owner)
        {
        }

        public override void Update(Wizard Owner)
        {

        }

        public override void Exit(Wizard Owner)
        {

        }

        public override void HandleStateChange(Wizard Owner)
        {
            CheckDie(Owner);
            if (!Owner.isStun)
            {
                CheckSkill(Owner);
                if (!Owner.isShieldCool)
                {
                    CheckRunRange(Owner);
                }

            }

        }

        public void CheckDie(Wizard Owner)
        {
            if (Owner.hpController.hp <= 0)
            {
                isDie = true;
                Owner.ChangeState(Wizard.State.Die);
            }
            else if (!isDie)
            {
                if (!Owner.isGround)
                {
                    Owner.characterController.Move(new Vector3(0, Physics.gravity.y, 0).normalized * Time.deltaTime);
                }
            }
            else
            {
                return;
            }
        }
        public void CheckRunRange(Wizard Owner)
        {
            GameObject runTarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.invincibleRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                runTarget = Owner.ChangeTarget(targets, true).gameObject;
                Owner.ChangeState(Wizard.State.Run);
                return;
            }
            else
            {
                runTarget = null;
            }
        }
        public void CheckSkill(Wizard Owner)
        {
            if (Owner.hpController.initMp > 0 && Owner.hpController.mp >= Owner.hpController.initMp)
            {
                Owner.ChangeState(Wizard.State.Skill);
            }
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(Wizard Owner)
        {
        }

        public override void Update(Wizard Owner)
        {
            GameObject findtarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.findRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                findtarget = Owner.ChangeTarget(targets, true).gameObject;
                Owner.ChangeState(Wizard.State.Trace);
                return;
            }
            else
            {
                findtarget = null;
            }

        }

        public override void Exit(Wizard Owner)
        {

        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(Wizard Owner)
        {

        }

        public override void Update(Wizard Owner)
        {
            //몬스터 공격
            GameObject attackTarget;
            Collider[] attackTargets = Physics.OverlapSphere(Owner.transform.position, Owner.attackRange, Owner.targetLayerMask);
            if (attackTargets.Length > 0)
            {
                attackTarget = Owner.ChangeTarget(attackTargets, true).gameObject;
                Owner.transform.LookAt(new Vector3(attackTarget.transform.position.x, Owner.transform.position.y, attackTarget.transform.position.z));
                Owner.ChangeState(Wizard.State.Attack);
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
                Owner.ChangeState(Wizard.State.Idle);
                return;
            }

            Vector3 moveDir = traceTarget.transform.position - Owner.transform.position;
            Owner.characterController.Move(new Vector3(moveDir.x, Physics.gravity.y, moveDir.z).normalized * Time.deltaTime * Owner.moveSpeed);
            Owner.transform.LookAt(new Vector3(traceTarget.transform.position.x, Owner.transform.position.y, traceTarget.transform.position.z));



        }

        public override void Exit(Wizard Owner)
        {
            Owner.animator.SetBool("isRun", false);
        }
    }

    public class AttackState : BaseState
    {
        private bool isAttacking;
        public override void Enter(Wizard Owner)
        {
        }

        public override void Update(Wizard Owner)
        {
            if (isAttacking == false)
            {
                Owner.StartCoroutine(AttackTime(Owner));
            }
        }

        public override void Exit(Wizard Owner)
        {
           
        }

        IEnumerator AttackTime(Wizard Owner)
        {
            isAttacking = true;
            Owner.AttackSound();
            Owner.animator.SetTrigger("Attack");
            yield return new WaitForSeconds(3.0f / Owner.attackController.attackSpeed);
            Owner.animator.ResetTrigger("Attack");
            isAttacking = false;
            Owner.ChangeState(Archer.State.Idle);
        }
    }

    public class RunState : BaseState
    {
        public override void Enter(Wizard Owner)
        {
            if(!Owner.isShieldCool)
            {
                Owner.StartCoroutine(RunRoutine(Owner));
            }
         

        }

        public override void Update(Wizard Owner)
        {


        }

        public override void Exit(Wizard Owner)
        {

        }

        IEnumerator RunRoutine(Wizard Owner)
        {
            Owner.isShieldCool = true;
            Owner.animator.SetTrigger("Invincible");
            Owner.animator.SetBool("isInvincible", true);
            yield return new WaitForSeconds(2f);
            Owner.animator.SetBool("isInvincible", false);
            yield return new WaitForSeconds(10f);
            Owner.isShieldCool = false;

        }
    }
    public class SkillState : BaseState
    {

        public override void Enter(Wizard Owner)
        {
            Owner.StartCoroutine(SkillRoutine(Owner));
        }

        public override void Update(Wizard Owner)
        {


        }

        public override void Exit(Wizard Owner)
        {
            Owner.animator.ResetTrigger("Skill");
        }

        IEnumerator SkillRoutine(Wizard Owner)
        {
            Owner.hpController.mp -= Owner.hpController.initMp;
            Owner.hpController.OnChangeMp(0);
            Owner.animator.SetTrigger("Skill");
            Owner.animator.SetInteger("RandomSkill", Random.Range(1, 3));
            yield return null;

            //Owner.ChangeState(Grunt.State.Idle);
        }
    }
    public class StunState : BaseState
    {
        public override void Enter(Wizard Owner)
        {
            Owner.isStun = true;
            Owner.StartCoroutine(StunTime(Owner));
        }

        public override void Update(Wizard Owner)
        {


        }

        public override void Exit(Wizard Owner)
        {

        }

        IEnumerator StunTime(Wizard Owner)
        {
            Owner.animator.SetBool("isStun", true);
            Owner.animator.SetTrigger("Stun");
            yield return new WaitForSeconds(2f);
            Owner.isStun = true;
            Owner.animator.SetBool("isStun", false);
            Owner.ChangeState(Wizard.State.Idle);
        }
    }

    public class DieState : BaseState
    {
        private bool isDie = true;
        public override void Enter(Wizard Owner)
        {
            if (isDie == true)
            {
                isDie = false;
                Owner.animator.SetTrigger("Die");
                Owner.animator.SetBool("isDie", true);
                Owner.DieCount();
                Owner.Die(1.5f);
            }
        }

        public override void Update(Wizard Owner)
        {

        }

        public override void Exit(Wizard Owner)
        {

        }
    }
}
