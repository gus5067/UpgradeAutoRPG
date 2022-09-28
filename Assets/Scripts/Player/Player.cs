using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(AudioSource))]
public abstract class Player : MonoBehaviour,IDamageable,ICanChangeTarget
{
    public enum State { Idle, Trace, Attack, Stun, Run, Skill, Die }

    [SerializeField]
    private LayerMask _targetLayerMask;
    public LayerMask targetLayerMask { get { return _targetLayerMask; } }

    [SerializeField]
    protected AudioClip[] clips;

    protected AudioSource curAudio;

    [SerializeField, Range(0f, 10f)]
    protected float _attackRange;
    public float attackRange { get { return _attackRange; } }

    [SerializeField, Range(0f, 15f)]
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
    public event UnityAction onChangeDie;
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
        Invoke("DieProgress", time);
    }

    public void DieProgress()
    {
        onChangeDie?.Invoke();
        gameObject.SetActive(false);
    }

    protected void Victory(bool isVic)
    {
        if (isVic)
        {
            if(animator != null)
            {
                animator.SetBool("isVic", true);
            }
            
        }
        else
        {
            return;
        }
    }
    public void AttackSound()
    {
        curAudio.clip = clips[Random.Range(0, clips.Length)];
        curAudio.Play();
    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _findRange);
    }
    public Collider ChangeTarget(Collider[] targets, bool isFindTargetMode)
    {
        if (isFindTargetMode)
        {
            if (targets.Length > 1)
            {
                float curValue;
                int curNum = 0;
                float shortValue = (transform.position - targets[0].transform.position).sqrMagnitude;
                for (int i = 0; i < targets.Length; i++)
                {
                    curValue = (transform.position - targets[i].transform.position).sqrMagnitude;
                    if (curValue < shortValue)
                    {
                        shortValue = curValue;
                        curNum = i;
                    }
                }
                return targets[curNum];
            }
            else
            {
                return targets[0];
            }
        }
        else
        {
            if (targets.Length > 1)
            {
                float curValue;
                int curNum = 0;
                float longValue = (transform.position - targets[0].transform.position).sqrMagnitude;
                for (int i = 0; i < targets.Length; i++)
                {
                    curValue = (transform.position - targets[i].transform.position).sqrMagnitude;
                    if (curValue > longValue)
                    {
                        longValue = curValue;
                        curNum = i;
                    }
                }
                return targets[curNum];
            }
            else
            {
                return targets[0];
            }
        }


    }
}
