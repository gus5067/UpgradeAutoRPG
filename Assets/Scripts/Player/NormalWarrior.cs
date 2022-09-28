using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NormalWarrior : Player,IDamageable,IStunable
{
    private StateMachine<State, NormalWarrior> stateMachine;


    [SerializeField]
    public TrailRenderer trailRenderer;

    [SerializeField]
    public GameObject[] playerSkill;

    private void Awake()
    {
        this.curAudio = GetComponent<AudioSource>();
        _attackController = GetComponent<AttackController>();
        _hpController = GetComponent<HpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, NormalWarrior>(this);

        stateMachine.AddState(State.Idle, new NormalWarriorStates.IdleState());
        stateMachine.AddState(State.Trace, new NormalWarriorStates.TraceState());
        stateMachine.AddState(State.Attack, new NormalWarriorStates.AttackState());
        stateMachine.AddState(State.Stun, new NormalWarriorStates.StunState());
        stateMachine.AddState(State.Die, new NormalWarriorStates.DieState());
        stateMachine.AddState(State.Skill, new NormalWarriorStates.SkillState());

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

    public void Stunned()
    {
       ChangeState(State.Stun);
    }



}
