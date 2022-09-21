using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serpent : Monster
{
    public enum State { Idle, Trace, Attack, Stun, Skill, Die }
    private StateMachine<State, Serpent> stateMachine;

    [SerializeField]
    private LayerMask _targetLayerMask;
    public LayerMask targetLayerMask { get { return _targetLayerMask; } }
    [SerializeField, Range(0f, 10f)]
    private float _attackRange;
    public float attackRange { get { return _attackRange; } }

    [SerializeField, Range(0f, 10f)]
    private float _rangeAttackRange;
    public float rangeAttackRange { get { return _rangeAttackRange; } }

    [SerializeField, Range(0f, 10f)]
    private float _findRange;
    public float findRange { get { return _findRange; } }

    [SerializeField, Range(0f, 10f)]
    private float _moveSpeed;
    public float moveSpeed { get { return _moveSpeed; } }

    [SerializeField]
    private GroundChecker groundChecker;

    private Animator _animator;
    public Animator animator { get { return _animator; } }

    private CharacterController _characterController;
    public CharacterController characterController { get { return _characterController; } }

    private monsterHpController _hpController;
    public monsterHpController hpController { get { return _hpController; } }


    public bool isGround;


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


    public void ChangeState(State nextState)
    {
        stateMachine.ChangeState(nextState);
    }
    public void Die(float time)
    {
        Destroy(gameObject, time);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _rangeAttackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _findRange);
    }
}
