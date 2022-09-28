using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anubis : Monster
{
    public Transform skillPoint; 

    [SerializeField]
    public GameObject skillPrefab;

    [SerializeField]
    public GameObject skillEffect;
    public enum State { Idle, Trace, Attack, Stun, Skill, Die }
    private StateMachine<State, Anubis> stateMachine;

    private void Awake()
    {
        curAudio = GetComponent<AudioSource>();
        _hpController = GetComponent<monsterHpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, Anubis>(this);

        stateMachine.AddState(State.Idle, new AnubisStates.IdleState());
        stateMachine.AddState(State.Trace, new AnubisStates.TraceState());
        stateMachine.AddState(State.Attack, new AnubisStates.AttackState());
        stateMachine.AddState(State.Stun, new AnubisStates.StunState());
        stateMachine.AddState(State.Die, new AnubisStates.DieState());
        stateMachine.AddState(State.Skill, new AnubisStates.SkillState());
    }

    private void Start()
    {
        StartCoroutine(AnubisRoutine());
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

    IEnumerator AnubisRoutine()
    {
        yield return new WaitForSeconds(4f);
        animator.SetTrigger("idleBreak");
        yield return new WaitForSeconds(2f);
        gameObject.layer = 6;
        ChangeState(State.Idle);
    }


    public void ChangeState(State nextState)
    {
        stateMachine.ChangeState(nextState);
    }
}
