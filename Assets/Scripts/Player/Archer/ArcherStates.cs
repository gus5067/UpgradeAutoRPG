using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcherStates
{
    public class BaseState : State<Archer>
    {
        private bool isDie = false;
        public override void Enter(Archer Owner)
        {
        }

        public override void Update(Archer Owner)
        {

        }

        public override void Exit(Archer Owner)
        {

        }

        public override void HandleStateChange(Archer Owner)
        {
            CheckDie(Owner);
            if(!Owner.isStun)
            {
                CheckSkill(Owner);
                CheckRunRange(Owner);
            }
          
        }

        public void CheckDie(Archer Owner)
        {
            if (Owner.hpController.hp <= 0)
            {
                isDie = true;
                Owner.ChangeState(Archer.State.Die);
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
        public void CheckRunRange(Archer Owner)
        {
            GameObject runTarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.runRange, Owner.targetLayerMask);
            if(targets.Length > 0)
            {
                runTarget = Owner.ChangeTarget(targets,true).gameObject;
                Owner.ChangeState(Archer.State.Run);
                return;
            }
            else
            {
                runTarget = null;
            }
        }
        public void CheckSkill(Archer Owner)
        {
            if (Owner.hpController.initMp > 0 && Owner.hpController.mp >= Owner.hpController.initMp)
            {
                Owner.ChangeState(Archer.State.Skill);
            }
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(Archer Owner)
        {
        }

        public override void Update(Archer Owner)
        {
            GameObject findtarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.findRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                findtarget = Owner.ChangeTarget(targets, true).gameObject;
                Owner.ChangeState(Archer.State.Trace);
                return;
            }
            else
            {
                findtarget = null;
            }

        }

        public override void Exit(Archer Owner)
        {

        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(Archer Owner)
        {

        }

        public override void Update(Archer Owner)
        {
            //몬스터 공격
            GameObject attackTarget;
            Collider[] attackTargets = Physics.OverlapSphere(Owner.transform.position, Owner.attackRange, Owner.targetLayerMask);
            if (attackTargets.Length > 0)
            {
                attackTarget = Owner.ChangeTarget(attackTargets, true).gameObject;
                Owner.transform.LookAt(new Vector3(attackTarget.transform.position.x, Owner.transform.position.y, attackTarget.transform.position.z));
                Owner.ChangeState(Archer.State.Attack);
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
                Owner.ChangeState(Archer.State.Idle);
                return;
            }

            Vector3 moveDir = traceTarget.transform.position - Owner.transform.position;
            Owner.characterController.Move(new Vector3(moveDir.x, Physics.gravity.y, moveDir.z).normalized * Time.deltaTime * Owner.moveSpeed);
            Owner.transform.LookAt(new Vector3(traceTarget.transform.position.x, Owner.transform.position.y, traceTarget.transform.position.z));



        }

        public override void Exit(Archer Owner)
        {
            Owner.animator.SetBool("isRun", false);
        }
    }

    public class AttackState : BaseState
    {
        private bool isAttacking;
        public override void Enter(Archer Owner)
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

        public override void Update(Archer Owner)
        {
            if (isAttacking == false)
            {
                Owner.StartCoroutine(AttackTime(Owner));
              
            }
        }

        public override void Exit(Archer Owner)
        {
            Owner.animator.ResetTrigger("Attack");
        }

        IEnumerator AttackTime(Archer Owner)
        {
            isAttacking = true;
            int randomNum = Random.Range(1, 3);
            Owner.animator.SetTrigger("Attack");
            Owner.animator.SetInteger("randomAttack", randomNum);
            yield return new WaitForSeconds(1.0f / Owner.attackController.attackSpeed);
            isAttacking = false;
            Owner.ChangeState(Archer.State.Idle);
        }
    }

    public class RunState : BaseState
    {
        private bool isRun;
        public override void Enter(Archer Owner)
        {
            Owner.animator.SetBool("isRun", true);
            if(isRun == false)
            {
                Owner.StartCoroutine(RunRoutine(Owner));
            }
            
        }

        public override void Update(Archer Owner)
        {


        }

        public override void Exit(Archer Owner)
        {
            Owner.animator.SetBool("isRun", false);
        }

        IEnumerator RunRoutine(Archer Owner)
        {
            isRun = true;
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            Owner.transform.forward = new Vector3(x, 0, y);
            for (float i = 0; i<1f; i+=0.01f)
            {
                Owner.characterController.Move(Owner.transform.forward * 0.01f * Owner.moveSpeed *2.5f);
                yield return new WaitForSeconds(0.01f);
            }
            isRun = false;
            Owner.ChangeState(Archer.State.Idle);
        }
    }
    public class SkillState : BaseState
    {

        public override void Enter(Archer Owner)
        {
            Owner.StartCoroutine(SkillRoutine(Owner));
        }

        public override void Update(Archer Owner)
        {


        }

        public override void Exit(Archer Owner)
        {
            Owner.animator.ResetTrigger("Skill");
        }

        IEnumerator SkillRoutine(Archer Owner)
        {
            Owner.hpController.mp -= Owner.hpController.initMp;
            Owner.hpController.OnChangeMp(0);
            Owner.animator.SetTrigger("Skill");
            yield return null;

            //Owner.ChangeState(Grunt.State.Idle);
        }
    }
    public class StunState : BaseState
    {
        public override void Enter(Archer Owner)
        {
            Owner.isStun = true;
            Owner.StartCoroutine(StunTime(Owner));
        }

        public override void Update(Archer Owner)
        {


        }

        public override void Exit(Archer Owner)
        {

        }

        IEnumerator StunTime(Archer Owner)
        {
            Owner.animator.SetBool("isStun", true);
            Owner.animator.SetTrigger("Stun");
            yield return new WaitForSeconds(2f);
            Owner.isStun = true;
            Owner.animator.SetBool("isStun", false);
            Owner.ChangeState(Archer.State.Idle);
        }
    }

    public class DieState : BaseState
    {
        private bool isDie = true;
        public override void Enter(Archer Owner)
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

        public override void Update(Archer Owner)
        {

        }

        public override void Exit(Archer Owner)
        {

        }
    }
}