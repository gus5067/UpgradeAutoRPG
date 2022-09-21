using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serpent : Monster
{
    public enum State { Idle, Trace, Attack, Stun, Skill, Die }
    private StateMachine<State, Serpent> stateMachine;

    [SerializeField]
    private GameObject serpentRay;
    
    [SerializeField, Range(0f, 10f)]
    private float _rangeAttackRange;
    public float rangeAttackRange { get { return _rangeAttackRange; } }


    private void Awake()
    {
        _hpController = GetComponent<monsterHpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, Serpent>(this);

        stateMachine.AddState(State.Idle, new SerpentStates.IdleState());
        stateMachine.AddState(State.Trace, new SerpentStates.TraceState());
        stateMachine.AddState(State.Attack, new SerpentStates.AttackState());
        stateMachine.AddState(State.Stun, new SerpentStates.StunState());
        stateMachine.AddState(State.Die, new SerpentStates.DieState());
        stateMachine.AddState(State.Skill, new SerpentStates.SkillState());
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
    
    public void Ray(bool isAct)
    {
        serpentRay.SetActive(isAct);
    }
    public void ChangeState(State nextState)
    {
        stateMachine.ChangeState(nextState);
    }
}
