using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Player : MonoBehaviour,IDamageable
{

    [SerializeField]
    public TrailRenderer trailRenderer;

    [SerializeField]
    public GameObject playerSkill;

    [SerializeField]
    private LayerMask _targetLayerMask;
    public LayerMask targetLayerMask { get { return _targetLayerMask; } }
    [SerializeField, Range(0f, 10f)]
    protected float _attackRange;
    public float attackRange { get { return _attackRange; } }

    [SerializeField, Range(0f, 10f)]
    protected float _findRange;
    public float findRange { get { return _findRange; } }

    [SerializeField, Range(0f, 10f)]
    protected float _moveSpeed;
    public float moveSpeed { get { return _moveSpeed; } }

    [SerializeField]
    protected GroundChecker groundChecker;

    protected Animator _animator;
    public Animator animator { get { return _animator; } }

    protected CharacterController _characterController;
    public CharacterController characterController { get { return _characterController; } }

    protected AttackController _attackController;

    public AttackController attackController { get { return _attackController; } }

    public bool isGround;

    protected HpController _hpController;
    public HpController hpController { get { return _hpController; } }
    public event UnityAction<int> onChangeHp;



    public void HitDamage(int damage)
    {
        onChangeHp?.Invoke(damage);

    }

    public void DieCount()
    {
        StageManager.characterCount--;
    }

    public void Die(float time)
    {
        Destroy(gameObject, time);
    }

    protected void Victory(bool isVic)
    {
        if (isVic)
        {
            animator.SetBool("isVic", true);
        }
        else
        {
            return;
        }
    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _findRange);
    }
}
