using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NormalWarriorStates
{

    public class BaseState : State<NormalWarrior>
    {
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
            if(!Owner.isGround)
            {
                Owner.characterController.Move(new Vector3(0, Physics.gravity.y, 0).normalized * Time.deltaTime);
            }

            if (Owner.hpController.hp <= 0)
            {
                Owner.ChangeState(NormalWarrior.State.Die);
            }
            else
            {
                return;
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
                findtarget = targets[0].gameObject;
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
            //몬스터 공격
            GameObject attackTarget;
            Collider[] attackTargets = Physics.OverlapSphere(Owner.transform.position, Owner.attackRange, Owner.targetLayerMask);
            if (attackTargets.Length > 0)
            {
                attackTarget = attackTargets[0].gameObject;
                Owner.ChangeState(NormalWarrior.State.Attack);
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
            int randomNum = Random.Range(1, 6);
            Owner.animator.SetTrigger("Attack");
            Owner.animator.SetInteger("randomAttack", randomNum);
            yield return new WaitForSeconds(1.0f);
            Owner.ChangeState(NormalWarrior.State.Idle);
        }
    }

public class StunState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {
    
        }

        public override void Update(NormalWarrior Owner)
        {
            

        }

        public override void Exit(NormalWarrior Owner)
        {

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
