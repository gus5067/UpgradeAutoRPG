using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Grunt : Monster,IDamageable
{
    public enum State { Idle, Trace, Attack, Stun, Die }
    private StateMachine<State, Grunt> stateMachine;


    [SerializeField]
    private LayerMask _targetLayerMask;
    public LayerMask targetLayerMask { get { return _targetLayerMask; } }
    [SerializeField, Range(0f, 10f)]
    private float _attackRange;
    public float attackRange { get { return _attackRange; } }

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

    public bool isGround;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, Grunt>(this);

        stateMachine.AddState(State.Idle, new GruntStates.IdleState());
        stateMachine.AddState(State.Trace, new GruntStates.TraceState());
        stateMachine.AddState(State.Attack, new GruntStates.AttackState());
        stateMachine.AddState(State.Stun, new GruntStates.StunState());
        stateMachine.AddState(State.Die, new GruntStates.DieState());

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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _findRange);
    }
}
