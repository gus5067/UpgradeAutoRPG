using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMan : Monster
{
    public enum State { Idle, Trace, Attack, Dodge, Skill, Die }
    private StateMachine<State, FishMan> stateMachine;

    [SerializeField]
    private GameObject fishMan;



    private void Awake()
    {
        _hpController = GetComponent<monsterHpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, FishMan>(this);

        stateMachine.AddState(State.Idle, new FishManStates.IdleState());
        stateMachine.AddState(State.Trace, new FishManStates.TraceState());
        stateMachine.AddState(State.Attack, new FishManStates.AttackState());
        stateMachine.AddState(State.Dodge, new FishManStates.DodgeState());
        stateMachine.AddState(State.Die, new FishManStates.DieState());
        stateMachine.AddState(State.Skill, new FishManStates.SkillState());
        stateMachine.ChangeState(State.Idle);
    }
    private void Update()
    {
        stateMachine.Update();
        if (groundChecker.IsGrounded)
        {
            isGround = true;
        }
        else
        {

            isGround = false;
        }
    }


    public void ChangeState(State nextState)
    {
        stateMachine.ChangeState(nextState);
    }


    public override void HitDamage(int damage)
    {
        int num = Random.Range(1, 5);
        if (num == 4)
        {
            ChangeState(State.Dodge);
            base.HitDamage(0);
        }
        base.HitDamage(damage);
    }

    public void SkillSummon()
    {
        StageManager.monsterCount++;
        Instantiate(fishMan, transform.position + Vector3.right * 1f, fishMan.transform.rotation);
    }

}
