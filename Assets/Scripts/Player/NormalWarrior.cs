using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWarrior : MonoBehaviour
{
    public enum State { Idle, Trace, Attack, RunAway, Hit, Die }
    private StateMachine<State, NormalWarrior> stateMachine;

    [SerializeField]
    private LayerMask _targetLayerMask;
    public LayerMask targetLayerMask { get { return _targetLayerMask; } }
    [SerializeField, Range(0f, 10f)]
    private float _attackRange;
    public float attackRange { get { return _attackRange; } }

    [SerializeField, Range(0f, 10f)]
    private float _RunRange;
    public float RunRange { get { return _RunRange; } }

    [SerializeField, Range(0f, 10f)]
    private float _moveSpeed;
    public float moveSpeed { get { return _moveSpeed; } }

    private Animator _animator;
    public Animator animator { get { return _animator; } }

    private CharacterController _characterController;
    public CharacterController characterController { get { return _characterController; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        stateMachine = new StateMachine<State, NormalWarrior>(this);

        stateMachine.AddState(State.Idle, new NormalWarriorStates.IdleState());
        stateMachine.AddState(State.Trace, new NormalWarriorStates.TraceState());
        stateMachine.AddState(State.Attack, new NormalWarriorStates.AttackState());
        stateMachine.AddState(State.RunAway, new NormalWarriorStates.RunAwayState());
        stateMachine.AddState(State.Hit, new NormalWarriorStates.HitState());
        stateMachine.AddState(State.Die, new NormalWarriorStates.DieState());

        stateMachine.ChangeState(State.Idle);
    }
    private void Update()
    {
        stateMachine.Update();
    }


    public void ChangeState(State nextState)
    {
        stateMachine.ChangeState(nextState);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _RunRange);
    }
}
