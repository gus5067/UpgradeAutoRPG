using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Grunt : Monster,IDamageable
{
    public enum State { Idle, Trace, Attack, Stun, Skill, Die }
    private StateMachine<State, Grunt> stateMachine;

    private void Awake()
    {
        _hpController = GetComponent<monsterHpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, Grunt>(this);

        stateMachine.AddState(State.Idle, new GruntStates.IdleState());
        stateMachine.AddState(State.Trace, new GruntStates.TraceState());
        stateMachine.AddState(State.Attack, new GruntStates.AttackState());
        stateMachine.AddState(State.Stun, new GruntStates.StunState());
        stateMachine.AddState(State.Die, new GruntStates.DieState());
        stateMachine.AddState(State.Skill, new GruntStates.SkillState());
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

}
