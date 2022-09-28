using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWorm : Monster, IDamageable
{
    public enum State { Idle, Trace, Attack, Stun, Skill, Die }
    private StateMachine<State, GiantWorm> stateMachine;

    [SerializeField]
    private GameObject _shotPrefab;

    public GameObject skillTarget;

    public GameObject ShotPrefab { get { return _shotPrefab; } }


    [SerializeField]
    private Transform _shotPoint;
    public Transform ShotPoint { get { return _shotPoint; } }

    private void Awake()
    {
        curAudio = GetComponent<AudioSource>();
        _hpController = GetComponent<monsterHpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, GiantWorm>(this);

        stateMachine.AddState(State.Idle, new GiantWormStates.IdleState());
        stateMachine.AddState(State.Trace, new GiantWormStates.TraceState());
        stateMachine.AddState(State.Attack, new GiantWormStates.AttackState());
        stateMachine.AddState(State.Stun, new GiantWormStates.StunState());
        stateMachine.AddState(State.Die, new GiantWormStates.DieState());
        stateMachine.AddState(State.Skill, new GiantWormStates.SkillState());
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
