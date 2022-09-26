using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Player
{
    private StateMachine<State, Wizard> stateMachine;

    [SerializeField, Range(0f, 10f)]
    public float invincibleRange;

    [SerializeField]
    public GameObject[] skills;

    public bool isStun;
    public bool isShieldCool = false;
    private void Awake()
    {
        _attackController = GetComponent<AttackController>();
        _hpController = GetComponent<HpController>();
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        stateMachine = new StateMachine<State, Wizard>(this);

        stateMachine.AddState(State.Idle, new WizardStates.IdleState());
        stateMachine.AddState(State.Trace, new WizardStates.TraceState());
        stateMachine.AddState(State.Attack, new WizardStates.AttackState());
        stateMachine.AddState(State.Stun, new WizardStates.StunState());
        stateMachine.AddState(State.Run, new WizardStates.RunState());
        stateMachine.AddState(State.Die, new WizardStates.DieState());
        stateMachine.AddState(State.Skill, new WizardStates.SkillState());

        stateMachine.ChangeState(State.Idle);

    }
    private void OnEnable()
    {
        StageManager.characterCount++;
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
        Gizmos.DrawWireSphere(transform.position, invincibleRange);
    }

    public void Stunned()
    {
        ChangeState(State.Stun);
    }

    public void FireAttack(GameObject target)
    {
        if(target.GetComponent<Monster>().hitState != Monster.HitState.Burn)
        {
            StartCoroutine(WizardAttackRoutine(target));
        }
    }
    IEnumerator WizardAttackRoutine(GameObject monster)
    {
        GameObject fire = Instantiate(skills[1], monster.transform.position, Quaternion.identity);
        fire.transform.SetParent(monster.transform);
        Monster target = monster.GetComponent<Monster>();
        target.hitState = Monster.HitState.Burn;
        for (int i = 0; i < 10; i++)
        {
            if (monster == null)
            {
                Destroy(fire.gameObject);
                yield return null;
            }
            target?.HitDamage(WeaponManager.Instance.minDamage/4);
            yield return new WaitForSeconds(1f);
        }
        target.hitState = Monster.HitState.Normal;
        Destroy(fire.gameObject);
    }

    public void Frozen(Monster monster)
    {
        
        StartCoroutine(WizardFrozenAttack(monster));
    }
    IEnumerator WizardFrozenAttack(Monster monster)
    {
        yield return new WaitForSeconds(0.5f);
        monster?.HitDamage(WeaponManager.Instance.minDamage);
        GameObject obj = Instantiate(skills[3], monster.transform);
        float temp = monster.attackTime;
        monster.attackTime *= 0.5f;
        yield return new WaitForSeconds(2f);
        Destroy(obj);
        yield return new WaitForSeconds(1f);
        monster.attackTime = temp;
    }
 
        
}
