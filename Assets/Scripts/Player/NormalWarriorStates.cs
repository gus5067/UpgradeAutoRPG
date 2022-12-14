using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NormalWarriorStates
{

    public class BaseState : State<NormalWarrior>
    {
        private bool isDie = false;
        public override void Enter(NormalWarrior Owner)
        {
        }

        public override void Update(NormalWarrior Owner)
        {

        }

        public override void Exit(NormalWarrior Owner)
        {

        }

        public override void HandleStateChange(NormalWarrior Owner)
        {
            CheckDie(Owner);
            CheckSkill(Owner);
        }

        public void CheckDie(NormalWarrior Owner)
        {

            if (Owner.hpController.hp <= 0)
            {
                isDie = true;
                Owner.ChangeState(NormalWarrior.State.Die);
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

        public void CheckSkill(NormalWarrior Owner)
        {
            if (Owner.hpController.initMp > 0 && Owner.hpController.mp >= Owner.hpController.initMp)
            {
                Owner.ChangeState(NormalWarrior.State.Skill);
            }
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {
        }

        public override void Update(NormalWarrior Owner)
        {
            GameObject findtarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.findRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                findtarget = Owner.ChangeTarget(targets, true).gameObject;
                Owner.ChangeState(NormalWarrior.State.Trace);
                return;
            }
            else
            {
                findtarget = null;
            }

        }

        public override void Exit(NormalWarrior Owner)
        {

        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {
           
        }

        public override void Update(NormalWarrior Owner)
        {
            //?????? ????
            GameObject attackTarget;
            Collider[] attackTargets = Physics.OverlapSphere(Owner.transform.position, Owner.attackRange, Owner.targetLayerMask);
            if (attackTargets.Length > 0)
            {
                attackTarget = Owner.ChangeTarget(attackTargets, true).gameObject;
                Owner.transform.LookAt(new Vector3(attackTarget.transform.position.x, Owner.transform.position.y, attackTarget.transform.position.z));
                Owner.ChangeState(NormalWarrior.State.Attack);
                return;
            }
            else
            {
                attackTarget = null;
            }
            //?????? ????
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
                Owner.ChangeState(NormalWarrior.State.Idle);
                return;
            }

            Vector3 moveDir = traceTarget.transform.position - Owner.transform.position;
            Owner.characterController.Move(new Vector3(moveDir.x, Physics.gravity.y, moveDir.z).normalized * Time.deltaTime * Owner.moveSpeed);
            Owner.transform.LookAt(new Vector3(traceTarget.transform.position.x, Owner.transform.position.y, traceTarget.transform.position.z));

            
           
        }

        public override void Exit(NormalWarrior Owner)
        {
            Owner.animator.SetBool("isRun", false);
        }
    }

    public class AttackState : BaseState
    {
        private bool isAttacking;
        public override void Enter(NormalWarrior Owner)
        {
        }

        public override void Update(NormalWarrior Owner)
        {
            if(isAttacking == false)
            {
                Owner.StartCoroutine(AttackTime(Owner));
           
            }
        }

        public override void Exit(NormalWarrior Owner)
        {
            Owner.animator.ResetTrigger("Attack");
        }

        IEnumerator AttackTime(NormalWarrior Owner)
        {
            isAttacking = true;
            int randomNum = Random.Range(1, 6);
            Owner.animator.SetTrigger("Attack");
            Owner.animator.SetInteger("randomAttack", randomNum);
            yield return new WaitForSeconds(1.0f/Owner.attackController.attackSpeed);
            isAttacking = false;
            Owner.ChangeState(NormalWarrior.State.Idle);
        }
    }
    public class SkillState : BaseState
    {

        public override void Enter(NormalWarrior Owner)
        {
            Owner.StartCoroutine(SkillRoutine(Owner));
        }

        public override void Update(NormalWarrior Owner)
        {


        }

        public override void Exit(NormalWarrior Owner)
        {
            Owner.animator.ResetTrigger("Skill");
        }

        IEnumerator SkillRoutine(NormalWarrior Owner)
        {
            Owner.hpController.mp -= Owner.hpController.initMp;
            Owner.hpController.OnChangeMp(0);
            Owner.animator.SetTrigger("Skill");
            Owner.animator.SetInteger("WeaponValue", WeaponManager.Instance.WeaponStateNum);
            yield return new WaitForSeconds(1f);
          
            //Owner.ChangeState(Grunt.State.Idle);
        }
    }
    public class StunState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {
            Owner.StartCoroutine(StunTime(Owner));
        }

        public override void Update(NormalWarrior Owner)
        {
            

        }

        public override void Exit(NormalWarrior Owner)
        {

        }

        IEnumerator StunTime(NormalWarrior Owner)
        {
            Owner.animator.SetBool("isStun", true);
            Owner.animator.SetTrigger("Stun");
            yield return new WaitForSeconds(2f);
            Owner.animator.SetBool("isStun", false);
            Owner.ChangeState(NormalWarrior.State.Idle);
        }
    }

    public class DieState : BaseState
    {
        private bool isDie = true;
        public override void Enter(NormalWarrior Owner)
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

        public override void Update(NormalWarrior Owner)
        {

        }

        public override void Exit(NormalWarrior Owner)
        {

        }
    }
}
