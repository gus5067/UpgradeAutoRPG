using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWorrior : Monster
{
    public enum State { Idle, Trace, Attack, Skill, Die }
    private StateMachine<State, EvilWorrior> stateMachine;

    [SerializeField]
    public GameObject skillSlash;

    [SerializeField]
    public GameObject skillRoar;
    private void Awake()
    {
        _hpController = GetComponent<monsterHpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, EvilWorrior>(this);

        stateMachine.AddState(State.Idle, new EvilWorriorStates.IdleState());
        stateMachine.AddState(State.Trace, new EvilWorriorStates.TraceState());
        stateMachine.AddState(State.Attack, new EvilWorriorStates.AttackState());
        stateMachine.AddState(State.Die, new EvilWorriorStates.DieState());
        stateMachine.AddState(State.Skill, new EvilWorriorStates.SkillState());
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
