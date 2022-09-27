using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class Monster : MonoBehaviour,IDamageable,ICanChangeTarget
{
    public enum HitState { Normal, Frozen, Burn }
    public HitState hitState = HitState.Normal;
    public event UnityAction<int> onChangeHp;
    public event UnityAction onChangeDie;
    [Range(0f, 1.4f)]
    public float attackTime;

    [SerializeField]
    protected DropItemData dropItemData;

    [SerializeField]
    protected LayerMask _targetLayerMask;
    public LayerMask targetLayerMask { get { return _targetLayerMask; } }
    [SerializeField, Range(0f, 10f)]
    protected float _attackRange;
    public float attackRange { get { return _attackRange; } }

    protected float _findRange = 15f;
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

    protected monsterHpController _hpController;
    public monsterHpController hpController { get { return _hpController; } }


    public bool isGround;
    public void Die(float time)
    {
        Invoke("DieProgress", time);
    }

    public void DieProgress()
    {
        onChangeDie?.Invoke();
        gameObject.SetActive(false);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _findRange);
    }
    public virtual void HitDamage(int damage)
    {
        onChangeHp?.Invoke(damage);
    }
    public void DropItem()
    {
        GameManager.Instance.gameMoney += Random.Range(dropItemData.minGold, dropItemData.maxGold);
        GameManager.Instance.gameGem += dropItemData.gem;
    }
    public void DieCount()
    {
        StageManager.monsterCount--;
    }
    public Collider ChangeTarget(Collider[] targets, bool isFindTargetMode)
    {
        if(isFindTargetMode)
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
