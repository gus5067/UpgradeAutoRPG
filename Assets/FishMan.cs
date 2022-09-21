using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMan : Monster
{
    public enum State { Idle, Trace, Attack, Dodge, Skill, Die }
    private StateMachine<State, FishMan> stateMachine;

    [SerializeField]
    private GameObject fishMan;
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

    private monsterHpController _hpController;
    public monsterHpController hpController { get { return _hpController; } }


    public bool isGround;


    private void Awake()
    {
        _hpController = GetComponent<monsterHpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, FishMan>(this);

        stateMachine.AddState(State.Idle, new FishManStates.IdleState());
        stateMachine.AddState(State.Trace, new FishManStates.TraceState());
        stateMachine.AddState(State.Attack, new FishManStates.AttackState());
        stateMachine.AddState(State.Dodge, new FishManStates.DodgeState());
        stateMachine.AddState(State.Die, new FishManStates.DieState());
        stateMachine.AddState(State.Skill, new FishManStates.SkillState());
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

    public override void HitDamage(int damage)
    {
        int num = Random.Range(1, 5);
        if (num == 4)
        {
            ChangeState(State.Dodge);
            base.HitDamage(0);
        }
        base.HitDamage(damage);
    }

    public void SkillSummon()
    {
        Instantiate(fishMan, transform.position + Vector3.right * 1f, fishMan.transform.rotation);
        StageManager.monsterCount++;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _findRange);
    }
}
