using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NormalWarrior : Player,IDamageable
{
    public enum State {Idle, Trace, Attack, Stun, Skill, Die }
    private StateMachine<State, NormalWarrior> stateMachine;


    [SerializeField]
    public TrailRenderer trailRenderer;

    [SerializeField]
    public GameObject[] playerSkill;

    private void Awake()
    {
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

    //public void Slash()
    //{
    //    Collider[] targets = Physics.OverlapSphere(transform.position, 15f, 6);
    //    if (targets.Length > 0)
    //    {
    //        for (int i = 0; i < targets.Length; i++)
    //        {
    //            Vector3 dirToTarget = (targets[i].transform.position - transform.position).normalized;
    //            if (Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(120 * 0.5f * Mathf.Deg2Rad))
    //            {
    //                Debug.Log("타겟이 시야 밖");
    //                continue; // 내적값이 작다는 것은 시야각보다 밖에 있다는 것 (코사인은 각도가 커질수록 값이 작아짐)
    //            }
    //            targets[i].GetComponent<Monster>().HitDamage(WeaponManager.Instance.minDamage * 5);
    //        }
    //    }
    //}
}
