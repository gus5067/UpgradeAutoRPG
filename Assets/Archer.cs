using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Player
{
    public enum State { Idle, Trace, Attack, Stun, Run, Skill, Die }
    private StateMachine<State, Archer> stateMachine;

    [SerializeField, Range(0f, 10f)]
    private float runRange;



    private void Awake()
    {
        _attackController = GetComponent<AttackController>();
        _hpController = GetComponent<HpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, Archer>(this);

        stateMachine.AddState(State.Idle, new ArcherStates.IdleState());
        stateMachine.AddState(State.Trace, new ArcherStates.TraceState());
        stateMachine.AddState(State.Attack, new ArcherStates.AttackState());
        stateMachine.AddState(State.Stun, new ArcherStates.StunState());
        stateMachine.AddState(State.Run, new ArcherStates.RunState());
        stateMachine.AddState(State.Die, new ArcherStates.DieState());
        stateMachine.AddState(State.Skill, new ArcherStates.SkillState());

        stateMachine.ChangeState(State.Idle);

        trailRenderer.enabled = false;
    }
    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        stageManager.onStageEnd += Victory;
        animator.SetBool("isVic", false);
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

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runRange);
    }
}
