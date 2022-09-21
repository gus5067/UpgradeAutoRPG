using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class Monster : MonoBehaviour,IDamageable
{

    public event UnityAction<int> onChangeHp;
    [Range(0f, 1.4f)]
    public float attackTime;

    [SerializeField]
    protected DropItemData dropItemData;

    private void Start()
    {
      
    }

    public void HitDamage(int damage)
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
}
