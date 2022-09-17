using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class Monster : MonoBehaviour,IDamageable
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int initHp;
    public event UnityAction<int> onChangeHp;


   

    public void HitDamage(int damage)
    {
        onChangeHp?.Invoke(damage);

    }

    public void DieCount()
    {
        StageManager.monsterCount--;
    }
}
