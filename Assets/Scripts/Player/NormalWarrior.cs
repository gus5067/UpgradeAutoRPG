using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NormalWarrior : MonoBehaviour,IDamageable
{
    public enum State {Idle, Trace, Attack, Stun, Die }
    private StateMachine<State, NormalWarrior> stateMachine;


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


    private float hp;
    private float initHp;
    private HpController hpController;

    public event UnityAction<float> onChangeHp;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, NormalWarrior>(this);

        stateMachine.AddState(State.Idle, new NormalWarriorStates.IdleState());
        stateMachine.AddState(State.Trace, new NormalWarriorStates.TraceState());
        stateMachine.AddState(State.Attack, new NormalWarriorStates.AttackState());
        stateMachine.AddState(State.Stun, new NormalWarriorStates.StunState());
        stateMachine.AddState(State.Die, new NormalWarriorStates.DieState());

        stateMachine.ChangeState(State.Idle);


        hpController = GetComponent<HpController>();

        hp = hpController.hp;
        initHp = hpController.initHp;
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

    public void HitDamage(float damage)
    {
        hp -= damage;
        onChangeHp?.Invoke(hp);
    }
}
