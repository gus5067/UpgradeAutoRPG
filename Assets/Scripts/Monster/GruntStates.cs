using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GruntStates
{

    public class BaseState : State<Grunt>
    {
        public override void Enter(Grunt Owner)
        {
        }

        public override void Update(Grunt Owner)
        {

        }

        public override void Exit(Grunt Owner)
        {

        }

        public override void HandleStateChange(Grunt Owner)
        {
            if (!Owner.isGround)
            {
                Owner.characterController.Move(new Vector3(0, Physics.gravity.y, 0).normalized * Time.deltaTime);
            }
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(Grunt Owner)
        {
            Debug.Log("idle 입장");
        }

        public override void Update(Grunt Owner)
        {
            GameObject findtarget;
            Collider[] targets = Physics.OverlapSphere(Owner.transform.position, Owner.findRange, Owner.targetLayerMask);
            if (targets.Length > 0)
            {
                findtarget = targets[0].gameObject;
                Owner.ChangeState(Grunt.State.Trace);
                return;
            }
            else
            {
                findtarget = null;
            }

        }

        public override void Exit(Grunt Owner)
        {

        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(Grunt Owner)
        {
            Debug.Log("Trace 입장");
        }

        public override void Update(Grunt Owner)
        {
            //몬스터 공격
            GameObject attackTarget;
            Collider[] attackTargets = Physics.OverlapSphere(Owner.transform.position, Owner.attackRange, Owner.targetLayerMask);
            if (attackTargets.Length > 0)
            {
                attackTarget = attackTargets[0].gameObject;
                Owner.ChangeState(Grunt.State.Attack);
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
                Owner.ChangeState(Grunt.State.Idle);
                return;
            }

            Vector3 moveDir = traceTarget.transform.position - Owner.transform.position;
            Owner.characterController.Move(new Vector3(moveDir.x, Physics.gravity.y, moveDir.z).normalized * Time.deltaTime * Owner.moveSpeed);
            Owner.transform.LookAt(new Vector3(traceTarget.transform.position.x, Owner.transform.position.y, traceTarget.transform.position.z));



        }

        public override void Exit(Grunt Owner)
        {
            Owner.animator.SetBool("isRun", false);
        }
    }

    public class AttackState : BaseState
    {
        public override void Enter(Grunt Owner)
        {
            Debug.Log("Attack 입장");
        }

        public override void Update(Grunt Owner)
        {
            int randomNum = Random.Range(1, 3);
            Owner.animator.SetTrigger("Attack");
            Owner.animator.SetInteger("randomAttack", randomNum);
            Owner.StartCoroutine(AttackTime(Owner));

        }

        public override void Exit(Grunt Owner)
        {
            Owner.animator.ResetTrigger("Attack");
        }

        IEnumerator AttackTime(Grunt Owner)
        {
            yield return new WaitForSeconds(1.0f);
            Owner.ChangeState(Grunt.State.Idle);
        }
    }

    public class StunState : BaseState
    {
        public override void Enter(Grunt Owner)
        {

        }

        public override void Update(Grunt Owner)
        {


        }

        public override void Exit(Grunt Owner)
        {

        }
    }

    public class DieState : BaseState
    {
        public override void Enter(Grunt Owner)
        {
        }

        public override void Update(Grunt Owner)
        {

        }

        public override void Exit(Grunt Owner)
        {

        }
    }
}
